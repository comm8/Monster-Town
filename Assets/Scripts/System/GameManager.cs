using UnityEngine;
using BuildingTools;
using Unity.Mathematics;
using UnityEngine.EventSystems;
using SerializableDictionary.Scripts;
using System.Collections.Generic;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;

    [Header("Tile Utilities")]
    [SerializeField] Transform cameraTransform;
    Inputactions3D inputActions;
    public int3 gridDimensions;
    public GameObject tilePrefab;
    public GameObject UnitSelectionPrefab;

    [Header("Selection")]
    public Transform Selection;
    public Transform selectionTweened;
    public MeshRenderer selectionRenderer;
    public Light selectionLight;
    public GameObject selectionHologram;

    public int3 SelectionGridPos = new(0, 0, 0);
    [SerializeField] BuildingType plyBuildingDesired;
    [HideInInspector] public bool pointerOverUI = false;

    [Header("Memory")]
    public BuildingData[] tiles;

    public List<MonsterStats> monsters;

    [Header("Other")]

    public BuildingList buildings;

    public SerializableDictionary<MonsterType, Sprite> imageDictionary;

    [SerializeField] SerializableDictionary<RoadTable, Vector2> roadShapeDictionary;

    [SerializeField] GameObject UI;

    bool[] bitmask;

    bool Interacting;
    bool deleteMode;
    bool deleteModeKeyDown;
    //public Texture2D heightMap;


    [Header("Interactions")]

    public InteractionMode deleteInteraction, standardInteraction, roadInteraction, unselectedInteraction;

    [HideInInspector] public InteractionMode interaction;

    public ResourceValue[] inventory;
    [SerializeField] TMP_Text[] resourceAmountsText;
    public int maxEnemies;
    [SerializeField] SpawnItem[] monsterSpawnChance;
    WeightedRandom spawnWeightedRandomMonster;

    [SerializeField] UnitSelectionMenu unitSelectionPanel;

    [SerializeField] Transform PopupBar;

    [SerializeField] GameObject PopupBubble;

    [SerializeField] GameObject ValidAreaTile;
    ValidAreaMap[] validAreas;

    public int gridSize => gridDimensions.x * gridDimensions.y * gridDimensions.z;

    public List<string> names;

    public Mesh buildingMesh;
    public Material buildingMaterial;

    public BuildingData currentTile;

    private void Awake()
    {
        instance = this;
        //update to only store buildings
        tiles = new BuildingData[gridSize];
        Debug.Log(tiles.Length);
        InvokeRepeating(nameof(UpdateTiles), 0.3f, 1f);

        bitmask = new bool[gridSize];
        Time.timeScale = 1;
        LeanTween.reset();



        deleteInteraction.gameManager = this;
        standardInteraction.gameManager = this;
        roadInteraction.gameManager = this;
        unselectedInteraction.gameManager = this;
        interaction = unselectedInteraction;
        //settup input system
        inputActions = new Inputactions3D();
        inputActions.Player.Enable();



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

        if (inputActions.Player.Escape.ReadValue<float>() > 0.5f)
        {
            SetInteractionMode(unselectedInteraction);
        }
        CheckInputDesired();
        UpdateResourceText(); //Make not update every frame
    }
    void UpdateSelectedTile()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            var newSelectionGridPos = BuildingUtils.PositionToTile(hit.point);
            Vector3 newPos = new Vector3(newSelectionGridPos.x, newSelectionGridPos.y, newSelectionGridPos.z) * 10;


            Debug.Log("true position is " + hit.point + ". Calculated position equals " + newPos);

            if (newPos != Selection.position)
            {
                interaction.OnTileExit(currentTile, plyBuildingDesired);
                SelectionGridPos = newSelectionGridPos;
                currentTile = tiles[BuildingUtils.CoordsToSlotID(SelectionGridPos, gridDimensions)];
                Selection.position = newPos;
                interaction.OnTileEnter(currentTile, plyBuildingDesired);
            }
        }
    }
    void UpdateResourceText()
    {
        resourceAmountsText[0].text = "<sprite name=\"wood\">" + inventory[0].Amount;
        resourceAmountsText[1].text = "<sprite name=\"charcoal\">" + inventory[1].Amount;
        resourceAmountsText[2].text = "<sprite name=\"stone\">" + inventory[2].Amount;
        resourceAmountsText[3].text = "<sprite name=\"metal\">" + inventory[3].Amount;
        resourceAmountsText[4].text = "<sprite name=\"rations\">" + inventory[4].Amount;
        resourceAmountsText[5].text = "<sprite name=\"influence\">" + inventory[5].Amount;
    }

    public void SetDesiredBuilding(int iD)
    {
        if (buildings.GetBuildingType(iD) == BuildingType.None)
        {
            if (deleteMode)
            {
                DisableDeleteMode(); return;
            }
            else
            {
                EnableDeleteMode(); return;
            }

        }
        DisableDeleteMode();

        if (buildings.GetBuildingType(iD) == BuildingType.Road)
        {
            SetInteractionMode(roadInteraction);
        }
        else
        {
            SetInteractionMode(standardInteraction);
        }

        plyBuildingDesired = buildings.GetBuildingType(iD);
    }

    void OnPressStart()
    {
        interaction.OnPressStart(currentTile, plyBuildingDesired);
    }
    void OnPressEnd()
    {
        interaction.OnPressEnd(currentTile, plyBuildingDesired);
    }
    void OnPress()
    {
        interaction.OnPress(currentTile, plyBuildingDesired);
    }



    public void UpdateBitMask(ushort id, bool value)
    {
        bitmask[id] = value;
        //get AI to update 
        Debug.Log("updated bitmask to" + value + " at " + BuildingUtils.SlotIDToCoords(id, 20));
    }

    public void RefreshUnitSelectionPanel(BuildingData tile)
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
        inventory[5].Amount = 0;
        foreach (var monster in monsters)
        {


            if (monster.tile == null) { continue; }

            if (Inventory.TryChargeCost(inventory, buildings.GetBuilding((int)monster.tile.buildingType).production[(int)monster.type].cost, true))
            {
                Inventory.AddToInventory(inventory, buildings.GetBuilding((int)monster.tile.buildingType).production[(int)monster.type].production);
            }

        }

        if (rollImmigration()) { GenerateMonster(); }
    }

    bool rollImmigration()
    {
        float randomamm = UnityEngine.Random.Range(1, 400);
        if (randomamm <= inventory[5].Amount)
        {
            return true;
        }
        return false;
    }

    public void SetMonsterEmploymentStatus(MonsterStats monster, int tileID)
    {
        var oldUnitID = tiles[tileID].monsterID;
        if (oldUnitID != 0)
        {
            monsters[oldUnitID].tile = null;
            unitSelectionPanel.panels[oldUnitID - 1].Setup(monsters[oldUnitID]);
        }

        tiles[tileID].monsterID = monster.ID;
        if (monster.tile != null)
        {
            var oldTile = monster.tile;
            oldTile.monsterID = 0;
            oldTile.UpdateMonsterEmployment();
        }
        monster.tile = tiles[tileID];
        if (unitSelectionPanel.currentTile.ID == tileID)
        {
            unitSelectionPanel.CurrentlyEmployedMonster = monster;
            unitSelectionPanel.UpdateBuildingPanel();
        }
        tiles[tileID].UpdateMonsterEmployment();

        unitSelectionPanel.panels[monster.ID - 1].Setup(monster);
    }

    public void UpdateMonsterEmploymentStatus(MonsterStats monster)
    {

    }

    void GenerateMonster()
    {
        var gridInit = GetComponent<InitializeLevel>();

        var myType = monsterSpawnChance[spawnWeightedRandomMonster.GetRandom()];

        monsters.Add(new MonsterStats { name = names[UnityEngine.Random.Range(0, names.Count - 1)], type = (MonsterType)myType.cost, icon = imageDictionary.Get((MonsterType)myType.cost), ID = (ushort)(monsters.Count) });
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
                DisableDeleteMode();
            }
        }
        else
        {
            if (inputActions.Player.Delete.ReadValue<float>() > 0.5f)
            {
                deleteModeKeyDown = true;
                EnableDeleteMode();
            }
        }
    }

    [ExecuteAlways]
    void DisableDeleteMode()
    {
        deleteMode = false;
        if (plyBuildingDesired != BuildingType.Road)
        {
            SetInteractionMode(standardInteraction);
        }
        else
        {
            SetInteractionMode(roadInteraction);
        }
    }


    [ExecuteAlways]
    void EnableDeleteMode()
    {
        deleteMode = true;
        SetInteractionMode(deleteInteraction);
    }





    public void SetInteractionMode(InteractionMode mode)
    {
        interaction.OnModeExit(currentTile, plyBuildingDesired);
        interaction = mode;
        interaction.OnModeEnter(currentTile, plyBuildingDesired);
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




    void OnTowerHit()
    {

    }




    public void SetSelectionScheme(SelectionScheme scheme)
    {
        selectionLight.color = scheme.lightColor;
        selectionRenderer.material = scheme.selectionMaterial;
    }

    public void SetAreaTheme(SelectionScheme scheme)
    {

    }

    public void UpdateArea()
    {

    }



    public void DevGive999()
    {
        foreach (var item in inventory)
        {
            item.Amount += 999;
        }
    }

    public void DevGive10Monsters()
    {
        for (int i = 0; i < 10; i++)
        {
            GenerateMonster();
        }
    }

    public void DevSetNight()
    {
        Instantiate(PopupBubble, PopupBar, false);
    }

    void OnDrawGizmos()
    {
        if (buildingMesh != null && buildingMaterial != null && tiles != null)
        {
            buildingMaterial.SetPass(0);
            foreach (var item in tiles)
            {
                Graphics.DrawMeshNow(buildingMesh, Matrix4x4.TRS( item.transform.position + new Vector3(0, 0.01f, 0), Quaternion.Euler(90, 0, 0), new Vector3(10, 10, 10)));
            }

        }
    }
}

