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
    public int2 SelectionGridPos;
    [SerializeField] BuildingType plyBuildingDesired;
    public bool pointerOverUI = false;

    [Header("Memory")]
    public TileProperties[] tileProperties;
    public List<MonsterStats> monsters;

    [Header("Other")]

    public BuildingList buildings;

    public SerializableDictionary<MonsterType, Sprite> imageDictionary;

    [SerializeField] SerializableDictionary<RoadTable, Vector2> roadShapeDictionary;

    [SerializeField] GameObject UI;

    bool Interacting;

    bool deleteMode;
    bool deleteModeKeyDown;

    [SerializeField] Material bulldozerPostProcessing;

    [SerializeField] ScriptableRendererFeature rendererFeature;

    public Texture2D heightMap;


    [Header("Interactions")]

    public InteractionMode deleteInteraction, standardInteraction, roadInteraction;

    public InteractionMode interaction;


    public ResourceValue[] inventory; 


 
    private void Awake()
    {
        instance = this;

        SetupInteractionModes();
        DevSpawnMonsters();

        //settup input system
        inputActions = new Inputactions3D();
        inputActions.Player.Enable();

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
    }


    void UpdatePlayerTileSelection()
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

    public void SetType(int iD)
    {
        if (buildings.GetBuildingType(iD) == BuildingType.None)
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

        if (buildings.GetBuildingType(iD) == BuildingType.Road)
        {
            interaction = roadInteraction;  
        }
        else
        {
            interaction = standardInteraction;
        }

        plyBuildingDesired = buildings.GetBuildingType(iD);
    }




    void OnPressStart()
    {
        var tile = GetCurrentTile();
        interaction.OnPressStart(tile, plyBuildingDesired);
    }
    void OnPressEnd()
    {
        var tile = GetCurrentTile();
        interaction.OnPressEnd(tile, plyBuildingDesired);
    }
    void OnPress()
    {
        var tile = GetCurrentTile();
        interaction.OnPress(tile, plyBuildingDesired);
    }

    TileProperties GetCurrentTile()
    {
        var curTile = BuildingUtils.CoordsToSlotID(SelectionGridPos, gridSize);

        if (!(curTile < tileProperties.Length && curTile >= 0)) { return null; }
        return tileProperties[curTile];
    }

    public void CreateUnitSelectionPanel(TileProperties tile)
    {

        var Menu = Instantiate(UnitSelectionPrefab, UI.transform).GetComponent<UnitSelectionMenu>();
        if (tile.monsterID == 0)
        {
            Menu.CurrentlyEmployedMonster = null;
        }
        else
        {
            Menu.CurrentlyEmployedMonster = monsters[tile.monsterID - 1];
        }

    }


    void DevSpawnMonsters()
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
        if (plyBuildingDesired != BuildingType.Road)
        {
            interaction = standardInteraction;
        }
        else
        {
            interaction = roadInteraction;
        }
    }


    [ExecuteAlways]
    void EnableBulldozer()
    {
        rendererFeature.SetActive(true);
        deleteMode = true;
        interaction = deleteInteraction;
    }


    void CheckInputDesired()
    {
        pointerOverUI = EventSystem.current.IsPointerOverGameObject();
        if (pointerOverUI) { return; }

        UpdatePlayerTileSelection();

        if (Interacting)
        {
            if (inputActions.Player.Fire.ReadValue<float>() < 0.5f)
            {
                OnPressEnd();
                Interacting = false;
            }
            else
            {
                OnPress();
            }
        }
        else
        {
            if (inputActions.Player.Fire.ReadValue<float>() > 0.5f)
            {
                OnPressStart();
                Interacting = true;
            }
        }
    }



    void SetupInteractionModes()
    {
        deleteInteraction = gameObject.AddComponent<DeleteInteraction>();
        standardInteraction = gameObject.AddComponent<StandardBuildInteraction>();
        roadInteraction = gameObject.AddComponent<RoadInteraction>();

        deleteInteraction.gameManager = this;
        standardInteraction.gameManager = this;
        roadInteraction.gameManager = this;

        interaction = standardInteraction;
    }

}

