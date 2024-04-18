using UnityEngine;
using BuildingTools;
using Unity.Mathematics;
using UnityEngine.EventSystems;
using SerializableDictionary.Scripts;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using TMPro;
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
    [HideInInspector] public bool pointerOverUI = false;

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

    //public Texture2D heightMap;


    [Header("Interactions")]

    public InteractionMode deleteInteraction, standardInteraction, roadInteraction;

    [HideInInspector] public InteractionMode interaction;

    public ResourceValue[] inventory;
    [SerializeField] TMP_Text[] resourceAmountsText;
    public int maxEnemies;


    [SerializeField] SpawnItem[] monsterSpawnChance;
    WeightedRandom spawnWeightedRandomMonster;

    [SerializeField] UnitSelectionMenu unitSelectionPanel;

    private void Awake()
    {
        instance = this;

        SetupInteractionModes();

        //settup input system
        inputActions = new Inputactions3D();
        inputActions.Player.Enable();

        //Init Tile array
        tileProperties = new TileProperties[gridSize * gridSize];
        InvokeRepeating(nameof(UpdateTiles), 0.3f, 1f);

        int[] ints = new int[monsterSpawnChance.Length];
        for (int i = 0; i < monsterSpawnChance.Length; i++)
        {
            ints[i] = monsterSpawnChance[i].weight;
        }

        spawnWeightedRandomMonster = new(ints);
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
        resourceAmountsText[0].text = "<sprite name=\"wood\">" + inventory[0].Amount;
        resourceAmountsText[1].text = "<sprite name=\"charcoal\">" + inventory[1].Amount;
        resourceAmountsText[2].text = "<sprite name=\"stone\">" + inventory[2].Amount;
        resourceAmountsText[3].text = "<sprite name=\"metal\">" + inventory[3].Amount;
        resourceAmountsText[4].text = "<sprite name=\"rations\">" + inventory[4].Amount;
        resourceAmountsText[5].text = "<sprite name=\"influence\">" + inventory[5].Amount;
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

    public void RefreshUnitSelectionPanel(TileProperties tile)
    {

        unitSelectionPanel.currentTile = tile;
        if (unitSelectionPanel.currentTile.monsterID == 0)
        {
            unitSelectionPanel.CurrentlyEmployedMonster = null;
        }
        else
        {
            unitSelectionPanel.CurrentlyEmployedMonster = monsters[unitSelectionPanel.currentTile.monsterID];
        }

        unitSelectionPanel.OpenMenu(tile);
    }


    void UpdateTiles()
    {

        foreach (var monster in monsters)
        {
            //inventory[7].Amount = 0;

            if (monster.tile == null) { continue; }

            if (Inventory.TryChargeCost(inventory, buildings.GetBuilding((int)monster.tile.buildingType).production[(int)monster.type].cost))
            {
                Inventory.AddToInventory(inventory, buildings.GetBuilding((int)monster.tile.buildingType).production[(int)monster.type].production);
            }

        }

        if (rollImmigration()) { GenerateMonster(); }


    }

    bool rollImmigration()
    {
        int chance = inventory[7].Amount;
        float randomamm = UnityEngine.Random.Range(1, 400);
        if (randomamm <= chance)
        {
            return true;
        }
        return false;
    }

    void GenerateMonster()
    {
        var gridInit = GetComponent<GridInitMono>();

        var myType = monsterSpawnChance[spawnWeightedRandomMonster.GetRandom()];
        monsters.Add(new MonsterStats { name = gridInit.Names[UnityEngine.Random.Range(0, 99)], type = (MonsterType)myType.cost, icon = imageDictionary.Get((MonsterType)myType.cost) });
        Debug.Log(myType.name);
        unitSelectionPanel.AddMonster(monsters.Count - 1);
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

