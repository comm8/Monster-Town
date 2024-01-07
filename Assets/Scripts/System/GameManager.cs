using UnityEngine;
using BuildingTools;
using Unity.Mathematics;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Tile Utilities")]

    [SerializeField] Transform cameraTransform;
    Inputactions3D inputActions;

    public int gridSize = 2;

    public GameObject tilePrefab;

    public ResourceTable farmResourceTable;
    public ResourceTable lumber_YardResourceTable;
    public ResourceTable mineResourceTable;
    public ResourceTable innResourceTable;
    public ResourceTable forgeResourceTable;
    public ResourceTable necroMansionResourceTable;
    public ResourceTable fishing_DockResourceTable;
    public ResourceTable light_HouseResourceTable;
    public ResourceTable apothecaryResourceTable;
    public ResourceTable armoryResourceTable;

    public TileProperties[] tileProperties;

    [Header("Day Night Cycle")]

    [SerializeField] Transform sunTransform, moonTransform;
    [SerializeField] Gradient SunColor, moonColor;

    [SerializeField] float speedMultiplier;
    [SerializeField] TMP_Text TextMesh;

    [Header("Selection")]

    [SerializeField] Transform Selection;
    [SerializeField] int2 SelectionGridPos;
    [SerializeField] BuildingType buildingType;

   public  bool pointerOverUI = false;

    public ModelList modelList;

    //
    private void Awake()
    {
        inputActions = new Inputactions3D();
        inputActions.Player.Enable();

        instance = this;
        tileProperties = new TileProperties[gridSize*gridSize];
    }

    void OnDestroy()
    {
        inputActions.Dispose();
    }

    void Update()
    {
        pointerOverUI = EventSystem.current.IsPointerOverGameObject();
        if(!pointerOverUI)
        {
        GetCurrentTile();
        SelectTile();
        }

        

        UpdateDayNightCycle();

    }

    void SelectTile()
    {

       if(inputActions.Player.Fire.ReadValue<float>() > 0.5f) 
        {
            var curTile = BuildingUtils.CoordsToSlotID(SelectionGridPos, gridSize);
            if(curTile < tileProperties.Length && curTile >= 0) 
            {
                placeTile(tileProperties[curTile], buildingType);
            }


        }
    }

    void GetCurrentTile()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        SelectionGridPos = BuildingUtils.PositionToTile(hit.point);
        Selection.position = new Vector3(SelectionGridPos.x * 10, 0, SelectionGridPos.y * 10);
    }

    void UpdateDayNightCycle()
    {
        float deltaTime = Time.deltaTime;

        sunTransform.Rotate(new Vector3(speedMultiplier, 0, 0) * deltaTime);
        moonTransform.Rotate(new Vector3(speedMultiplier, 0, 0) * deltaTime);
        int curtime = (int)(Time.time/2);
        TextMesh.text = curtime/6  + ":" + (curtime % 6) + "0";
    }

    public void SetType(string type)
    {
        BuildingType building = BuildingType.Farm;

        switch(type)
        {
            case "none" :
                building = BuildingType.None;
                break;

            case "farm":
                building = BuildingType.Farm;
                break;

            case "inn":
                building = BuildingType.Inn;
                break;

            case "lumber yard":
                building = BuildingType.Lumber_Yard;
                break;

            case "fishing dock":
                building = BuildingType.Fishing_Dock;
                break;

            case "apothecary":
                building = BuildingType.Apothecary;
                break;

            case "forge":
                building = BuildingType.Forge;
                break;

            case "lighthouse":
                building = BuildingType.Light_House;
                break;

            case "necromansion":
                building = BuildingType.NecroMansion;
                break;

            case "mine":
                building = BuildingType.Mine;
                break;
        }

        SetType(building);
    }

    public void SetType(BuildingType type)
    {
        buildingType = type;
    }



    public void placeTile(TileProperties tile, BuildingType desired)
    {
        Destroy(tile.model);
        GameObject desiredModel = modelList.EmptyPlot;

        switch (desired)
        {
            case BuildingType.Farm:
                desiredModel = modelList.FarmModel; break;

            case BuildingType.Lumber_Yard: 
                desiredModel = modelList.LumberModel; break;

            case BuildingType.Mine:
                desiredModel = modelList.MineModel; break;

            case BuildingType.NecroMansion:
                desiredModel = modelList.Necromansion; break;

            case BuildingType.Inn:
                desiredModel = modelList.InnModel; break;

            case BuildingType.Forge:
                desiredModel = modelList.ForgeModel; break;

            case BuildingType.Apothecary:
                desiredModel = modelList.ApothecaryModel; break;
        }
        tile.model = Instantiate(desiredModel, tile.modelTransform);
        tile.GetComponentInChildren<TileAnimator>().playUpdateAnimation();
    }


}

[Serializable]
public class ModelList
{
    public GameObject EmptyPlot;
    public GameObject FarmModel;
    public GameObject InnModel;
    public GameObject LumberModel;
    public GameObject FishingModel;
    public GameObject ForgeModel;
    public GameObject ApothecaryModel;
    public GameObject LighthouseModel;
    public GameObject Necromansion;
    public GameObject MineModel;
    
}

