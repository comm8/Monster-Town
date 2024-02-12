using UnityEngine;
using BuildingTools;
using Unity.Mathematics;
using UnityEngine.EventSystems;
using SerializableDictionary.Scripts;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;

    [Header("Tile Utilities")]
    [SerializeField] Transform cameraTransform;
    Inputactions3D inputActions;
    public int gridSize = 20;
    public GameObject tilePrefab;
    public GameObject UnitSelectionPrefab;

    [Header("Selection")]
    [SerializeField] Transform Selection;
    [SerializeField] int2 SelectionGridPos;
    [SerializeField] BuildingType plyBuildingDesired;
    public bool pointerOverUI = false;
    List<int2> CurrentRoadStroke;


    [Header("Memory")]
    public TileProperties[] tileProperties;
    public List<MonsterStats> monsters;

    [Header("Other")]
    public SerializableDictionary<BuildingStats, ResourceValue[]> buildingOutputLookup;
    public SerializableDictionary<string, BuildingType> buildingNameDictionary;
    public SerializableDictionary<BuildingType, GameObject> modelDictionary;

    public SerializableDictionary<MonsterType, Sprite> imageDictionary;

    [SerializeField] SerializableDictionary<RoadTable, Vector2> roadShapeDictionary;

    [SerializeField] Transform border;

    [SerializeField] GameObject UI;

    bool Interacting;

    bool deleteMode;
    bool deleteModeKeyDown;

    [SerializeField] Material bulldozerPostProcessing;

    [SerializeField] ScriptableRendererFeature rendererFeature;

    public Texture2D heightMap;


    public InteractionMode deleteInteraction, standardInteraction, roadInteraction;

    public InteractionMode interaction;



    //
    private void Awake()
    {
        instance = this;

        deleteInteraction = gameObject.AddComponent<DeleteInteraction>();
        standardInteraction = gameObject.AddComponent<StandardBuildInteraction>();
        roadInteraction = gameObject.AddComponent<RoadInteraction>();

        interaction = standardInteraction;

        DevSetUpMonsters();

        //settup input system
        inputActions = new Inputactions3D();
        inputActions.Player.Enable();

        //set world border  graphic
        border.localScale = 10 * gridSize * Vector3.one;
        border.position = new Vector3(gridSize / 2, 0, gridSize / 2) * 10 - new Vector3(5, 0, 5);

        //Init Tile array
        tileProperties = new TileProperties[gridSize * gridSize];

        CurrentRoadStroke = new();
    }

    void OnDestroy()
    {
        instance = null;
        inputActions.Dispose();
    }

    void Update()
    {
        CheckDeleteModeDesired();
        CheckInputDesired();

        DayNightCycle.instance.UpdateDayNightCycle();
    }


    void UpdateSelectedTile()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            SelectionGridPos = BuildingUtils.PositionToTile(hit.point);
            Vector3 newPos = new Vector3(SelectionGridPos.x, 0, SelectionGridPos.y) * 10;

            if (newPos != Selection.position)
            {
                Selection.position = newPos;
            }
        }
    }


    bool CheckIfPlacementAllowed()
    {
        return !(GetCurrentTile().buildingType != BuildingType.None && !deleteMode);
    }

    public void SetType(string type)
    {
        if (buildingNameDictionary.Get(type) == BuildingType.None)
        {
            if (deleteMode)
            {
                DisableBulldozer(); return;
            }
            else
            {
                EnableBulldozer(); return;
            }

        }
        DisableBulldozer();

        plyBuildingDesired = buildingNameDictionary.Get(type);
    }

    public void UpdateRoad(TileProperties tile)
    {
        if (!tile.gameObject.TryGetComponent(out RoadProperties road))
        {
            road = tile.gameObject.AddComponent<RoadProperties>();
        }
        var roadTable = road.table;

        RoadTable inverseRoadTable = new();

        if (CurrentRoadStroke.Count > 0)
        {
            if (CurrentRoadStroke[^1].Equals(SelectionGridPos))
            {
                CurrentRoadStroke.Remove(CurrentRoadStroke.Count - 1);
                return;
            }
        }

        CurrentRoadStroke.Add(SelectionGridPos);


        if (CurrentRoadStroke.Count > 1)
        {
            int2 previousRoad = CurrentRoadStroke[^2];

            if (SelectionGridPos.x > previousRoad.x)
            {
                roadTable.right = true;
                inverseRoadTable.left = true;
            }
            else if (SelectionGridPos.x < previousRoad.x)
            {
                roadTable.left = true;
                inverseRoadTable.right = true;
            }
            else if (SelectionGridPos.y > previousRoad.y)
            {
                roadTable.up = true;
                inverseRoadTable.down = true;
            }
            else
            {
                roadTable.down = true;
                inverseRoadTable.up = true;
            }

            road.GetComponentInChildren<Renderer>().material = Resources.Load<Material>("road/road_" + BuildingUtils.toNumeralString(roadTable.up) + BuildingUtils.toNumeralString(roadTable.down) + BuildingUtils.toNumeralString(roadTable.left) + BuildingUtils.toNumeralString(roadTable.right));



            //adjust last road
            road = tileProperties[BuildingUtils.CoordsToSlotID(CurrentRoadStroke[^2], gridSize)].GetComponent<RoadProperties>();
            roadTable = road.table;

            if (roadTable.up || inverseRoadTable.up) { roadTable.up = true; }
            if (roadTable.down || inverseRoadTable.down) { roadTable.down = true; }
            if (roadTable.left || inverseRoadTable.left) { roadTable.left = true; }
            if (roadTable.right || inverseRoadTable.right) { roadTable.right = true; }

            road.GetComponentInChildren<Renderer>().material = Resources.Load<Material>("road/road_" + BuildingUtils.toNumeralString(roadTable.up) + BuildingUtils.toNumeralString(roadTable.down) + BuildingUtils.toNumeralString(roadTable.left) + BuildingUtils.toNumeralString(roadTable.right));
        }
    }



    public void PlaceTile(TileProperties tile, BuildingType desired)
    {
        if (deleteMode) { desired = BuildingType.None; }

        Destroy(tile.model);
        GameObject desiredModel = modelDictionary.Get(desired);

        tile.model = Instantiate(desiredModel, tile.modelTransform);
        tile.buildingType = desired;
        if (desired == BuildingType.Road)
        {
            UpdateRoad(tile);
        }
        tile.GetComponentInChildren<TileAnimator>().playUpdateAnimation();
    }

    public void InteractWithTile(TileProperties tile)
    {
        if (tile.buildingType == BuildingType.Road)
        {
            UpdateRoad(tile);
            return;
        }
        CreateUnitSelectionPanel();
    }

    void OnStartInteract()
    {
        if (!CheckIfPlacementAllowed())
        {

            InteractWithTile(GetCurrentTile());
        }
    }

    void OnEndInteract()
    {
        CurrentRoadStroke = new();
    }

    TileProperties GetCurrentTile()
    {
        var curTile = BuildingUtils.CoordsToSlotID(SelectionGridPos, gridSize);

        if (!(curTile < tileProperties.Length && curTile >= 0)) { return null; }
        return tileProperties[curTile];
    }


    /// <summary>
    /// Not called on first frame.
    /// </summary>
    void OnInteract()
    {
        var tile = GetCurrentTile();
        tile.GetComponentInChildren<TileAnimator>().playUpdateAnimation();

        if (tile.buildingType == BuildingType.Road && plyBuildingDesired == BuildingType.Road)
        {
            UpdateRoad(tile);
            return;
        }


        if (CheckIfPlacementAllowed())
        {

            PlaceTile(tile, plyBuildingDesired);
        }
    }

    void CreateUnitSelectionPanel()
    {
        Instantiate(UnitSelectionPrefab, UI.transform);
    }


    void DevSetUpMonsters()
    {
        var gridInit = GetComponent<GridInitMono>();
        monsters = new();
        for (int i = 0; i < 50; i++)
        {
            MonsterType myType = (MonsterType)UnityEngine.Random.Range(0, 9);
            monsters.Add(new MonsterStats { name = gridInit.Names[UnityEngine.Random.Range(0, 99)], type = myType, icon = imageDictionary.Get(myType) });
        }

    }

    void CheckDeleteModeDesired()
    {
        if (deleteModeKeyDown)
        {
            if (inputActions.Player.Delete.ReadValue<float>() < 0.5f)
            {
                deleteModeKeyDown = false;
                DisableBulldozer();
            }
        }
        else
        {
            if (inputActions.Player.Delete.ReadValue<float>() > 0.5f)
            {
                deleteModeKeyDown = true;
                EnableBulldozer();
            }
        }
    }

    [ExecuteAlways]
    void DisableBulldozer()
    {
        rendererFeature.SetActive(false);
        deleteMode = false;
    }


    [ExecuteAlways]
    void EnableBulldozer()
    {
        rendererFeature.SetActive(true);
        deleteMode = true;
    }


    void CheckInputDesired()
    {
        pointerOverUI = EventSystem.current.IsPointerOverGameObject();
        if (pointerOverUI) { return; }

        UpdateSelectedTile();

        if (Interacting)
        {
            if (inputActions.Player.Fire.ReadValue<float>() < 0.5f)
            {
                OnEndInteract();
                Interacting = false;
            }
            else
            {
                OnInteract();
            }
        }
        else
        {
            if (inputActions.Player.Fire.ReadValue<float>() > 0.5f)
            {
                OnStartInteract();
                Interacting = true;
            }
        }
    }
}

