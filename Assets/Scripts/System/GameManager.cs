using UnityEngine;
using BuildingTools;
using Unity.Mathematics;
using UnityEngine.EventSystems;
using SerializableDictionary.Scripts;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;

    [Header("Tile Utilities")]
    [SerializeField] Transform cameraTransform;
    Inputactions3D inputActions;
    public int gridSize = 20;
    public GameObject tilePrefab;

    [Header("Selection")]
    [SerializeField] Transform Selection;
    [SerializeField] int2 SelectionGridPos;
    [SerializeField] BuildingType buildingType;
    public bool pointerOverUI = false;
    int2[] buildingDragHistory; 


    [Header("Memory")]
    public TileProperties[] tileProperties;
    public MonsterStats[] monsters;

    [Header("Other")]
    public SerializableDictionary<BuildingStats, ResourceValue[]> buildingOutputLookup;
    public SerializableDictionary<string, BuildingType> buildingNameDictionary;
    public SerializableDictionary<BuildingType, GameObject> modelDictionary;

    [SerializeField] SerializableDictionary<RoadTable, Vector2> roadShapeDictionary;

    [SerializeField] Transform border;

    bool Interacting;
    //
    private void Awake()
    {
        instance = this;

        //settup input system
        inputActions = new Inputactions3D();
        inputActions.Player.Enable();

        //set world border  graphic
        border.localScale = Vector3.one * gridSize * 10;
        border.position = new Vector3 (gridSize/2, 0, gridSize / 2) *10 - new Vector3(5,0,5);

        //Init Tile array
        tileProperties = new TileProperties[gridSize*gridSize];
    }

    void OnDestroy()
    {
        instance = null;
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

        DayNightCycle.instance.UpdateDayNightCycle();

    }

    void SelectTile()
    {
       if(Interacting) 
        {
            var curTile = BuildingUtils.CoordsToSlotID(SelectionGridPos, gridSize);
            if(curTile < tileProperties.Length && curTile >= 0) 
            {
                var tile = tileProperties[curTile];
                if (tile.buildingType != BuildingType.None && buildingType != BuildingType.None)
                {
                    interactWithTile(tile); 
                }
                else
                {
                    placeTile(tileProperties[curTile], buildingType);
                }
            }
        }
    }

    void GetCurrentTile()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit);
        SelectionGridPos = BuildingUtils.PositionToTile(hit.point);
        Selection.position = new Vector3(SelectionGridPos.x, 0, SelectionGridPos.y) * 10;
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
        tile.buildingType = desired;
        tile.GetComponentInChildren<TileAnimator>().playUpdateAnimation();
    }

    public void interactWithTile(TileProperties tile)
    {
        tile.GetComponentInChildren<TileAnimator>().playUpdateAnimation();
    }

    void onStartPlace()
    {

    }

    void onEndPlace()
    {

    }

    void OnEnterTile()
    {

    }

    void OnExitTile()
    {

    }

    void checkLeftClick()
    {
        if(Interacting)
        {
            if(inputActions.Player.Fire.ReadValue<float>() < 0.5f)
            { 
                onEndPlace();
                Interacting = false;
            }
        }
        else
        {
            if (inputActions.Player.Fire.ReadValue<float>() > 0.5f)
            {
                onStartPlace();
                Interacting = true;
            }
        }
    }

}

