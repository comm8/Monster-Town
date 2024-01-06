using UnityEngine;
using BuildingTools;
using Unity.Mathematics;
using TMPro;

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

        GetCurrentTile();

        

        UpdateDayNightCycle();
        SelectTile();
    }

    void SelectTile()
    {
       if(inputActions.Player.Fire.ReadValue<float>() > 0.5f) 
        {
            var curTile = tileProperties[BuildingUtils.CoordsToSlotID(SelectionGridPos, gridSize)];
            //Object.Destroy(curTile.gameObject);
            curTile.GetComponentInChildren<TileAnimator>().playUpdateAnimation();
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

    public void SetTypeFarm()
    {
        buildingType = BuildingType.Farm;
        Debug.Log(buildingType);
    }

    public void SetTypeEmpty()
    {
        buildingType = BuildingType.None;
        Debug.Log(buildingType);
    }



}

