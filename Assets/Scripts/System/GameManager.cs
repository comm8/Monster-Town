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
    List<int2> buildingDragHistory;


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

    //
    private void Awake()
    {
        instance = this;

        DevSetUpMonsters();

        //settup input system
        inputActions = new Inputactions3D();
        inputActions.Player.Enable();

        //set world border  graphic
        border.localScale = Vector3.one * gridSize * 10;
        border.position = new Vector3(gridSize / 2, 0, gridSize / 2) * 10 - new Vector3(5, 0, 5);

        //Init Tile array
        tileProperties = new TileProperties[gridSize * gridSize];
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


    void UpdateCurrentTile()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            SelectionGridPos = BuildingUtils.PositionToTile(hit.point);
            Vector3 newPos = new Vector3(SelectionGridPos.x, 0, SelectionGridPos.y) * 10;

            if (newPos != Selection.position)
            {
                Selection.position = newPos;
                OnChangeTile();
            }
        }


    }


    bool CheckIfPlacementDesired()
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

    public void PlaceRoad(int2 tilepos)
    {
        if (buildingDragHistory[buildingDragHistory.Count - 1].Equals(tilepos))
        {
            buildingDragHistory.Remove(buildingDragHistory.Count - 1);
            return;
        }

        //set end point a
        //set end point b

    }


    public void placeTile(TileProperties tile, BuildingType desired)
    {
        if (deleteMode) { desired = BuildingType.None; }

        Destroy(tile.model);
        GameObject desiredModel = modelDictionary.Get(desired);

        tile.model = Instantiate(desiredModel, tile.modelTransform);
        tile.buildingType = desired;
        tile.GetComponentInChildren<TileAnimator>().playUpdateAnimation();
    }

    public void interactWithTile(TileProperties tile)
    {
        CreateUnitSelectionPanel();
    }

    void OnStartInteract()
    {
        if (!CheckIfPlacementDesired())
        {
            interactWithTile(GetCurrentTile());
        }
    }

    void OnEndInteract()
    {

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
        if (CheckIfPlacementDesired())
        {
            placeTile(tile, plyBuildingDesired);
        }
    }
    void OnChangeTile()
    {

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

        UpdateCurrentTile();

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

