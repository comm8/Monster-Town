using UnityEngine;
using BuildingTools;
using Unity.Mathematics;
using TMPro;
using UnityEngine.EventSystems;
using System;
using SerializableDictionary.Scripts;

public class GameManager : MonoBehaviour
{
  
    [HideInInspector] public static GameManager instance;

    [Header("Tile Utilities")]

    [SerializeField] Transform cameraTransform;
    Inputactions3D inputActions;

    public int gridSize = 20;

    public GameObject tilePrefab;



    [Header("Day Night Cycle")]

    [SerializeField] Transform sunTransform, moonTransform;
    [SerializeField] Gradient SunColor, moonColor;

    [SerializeField] float speedMultiplier;
    [SerializeField] TMP_Text TextMesh;

    [Header("Selection")]

    [SerializeField] Transform Selection;
    [SerializeField] int2 SelectionGridPos;
    [SerializeField] BuildingType buildingType;
    public bool pointerOverUI = false;
    int2[] buildingDragHistory; 


    [Header("Other")]

    public TileProperties[] tileProperties;
    public SerializableDictionary<BuildingStats, ResourceValue[]> buildingOutputLookup;
    public SerializableDictionary<string, BuildingType> buildingNameDictionary;
    public SerializableDictionary<BuildingType, GameObject> modelDictionary;

    [SerializeField] SerializableDictionary<RoadTable, Vector2> roadShapeDictionary;

    [SerializeField] Transform border;

    bool tryPlace;
    //
    private void Awake()
    {
        inputActions = new Inputactions3D();
        inputActions.Player.Enable();

        border.localScale = Vector3.one * gridSize * 10;
        border.position = new Vector3 (gridSize/2, 0, gridSize / 2) *10 - new Vector3(5,0,5);

        instance = this;
        tileProperties = new TileProperties[gridSize*gridSize];
    }

    void OnDestroy()
    {
        inputActions.Dispose();
    }

    void Update()
    {
        checkLeftClick();

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
       if(tryPlace) 
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
        buildingType = buildingNameDictionary.Get(type);
    }



    public void placeTile(TileProperties tile, BuildingType desired)
    {
        Destroy(tile.model);
        GameObject desiredModel = modelDictionary.Get(desired);

        tile.model = Instantiate(desiredModel, tile.modelTransform);
        tile.GetComponentInChildren<TileAnimator>().playUpdateAnimation();
    }


    void onStartPlace()
    {

    }

    void onEndPlace()
    {

    }

    void checkLeftClick()
    {
        if(tryPlace)
        {
            if(inputActions.Player.Fire.ReadValue<float>() < 0.5f)
            { 
                onEndPlace();
                tryPlace = false;
            }
        }
        else
        {
            if (inputActions.Player.Fire.ReadValue<float>() > 0.5f)
            {
                onStartPlace();
                tryPlace = true;
            }
        }
    }

}

[Serializable]
public class BuildingStats
{
    public MonsterType monsterType;
    public BuildingType buildingType;
}