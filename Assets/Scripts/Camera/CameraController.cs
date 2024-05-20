using BuildingTools;
using Unity.Mathematics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("WASD Movement")]
    [SerializeField] float movementMultiplier;
    Inputactions3D inputActions;

    [SerializeField] AnimationCurve accelerationCurve;
    [SerializeField] float accelerationTime;
    float accelerationTimer;

    [Header("Zoom Settings")]
    [SerializeField] float zoomPercentage;
    [SerializeField] AnimationCurve zoomRotationCurve;
    [SerializeField] AnimationCurve ZoomAltitudeCurve;


    [Header("Camera Clamping ")]
    [SerializeField] float MinAltitude, MaxAltitude;
    [SerializeField] float MinRotation, MaxRotation;


    [Header("Transforms")]
    [SerializeField] Transform rotFreeTransform, cameraTech;


    [Header("Rotation Dampening")]
    [SerializeField] float rotationVelocityDecay;
    [SerializeField] float rotationVelocity;

    [SerializeField] bool usingController;

    bool allowRotation;

    void Awake()
    {
        inputActions = new Inputactions3D();
        inputActions.Player.Enable();

    }
    void OnDestroy()
    {
        inputActions.Dispose();
    }



    private void UpdateTimer()
    {
        if (inputActions.Player.Move.ReadValue<Vector2>().sqrMagnitude > Mathf.Epsilon)
        {
            accelerationTimer += Time.deltaTime;
        }
        else
        {
            accelerationTimer = 0;
        }
    }

    private void CheckAllowRotation()
    {
        allowRotation = inputActions.Player.RotationMode.ReadValue<float>() > .02f || usingController;
    }

    private float ClampDeltaScroll()
    {
        float deltaZoom = 0;

        if (!GameManager.instance.pointerOverUI)
        {
            deltaZoom = inputActions.Player.Scroll.ReadValue<float>();
        }

        if (allowRotation)
        {
            deltaZoom += inputActions.Player.Look.ReadValue<Vector2>().y;
        }

        if (deltaZoom + zoomPercentage > 1 || deltaZoom + zoomPercentage < 0)
        {
            deltaZoom = 0;
        }

        return deltaZoom;
    }

    private void RotateCamera()
    {

        rotationVelocity = inputActions.Player.Rotate.ReadValue<float>() + inputActions.Player.ScrollHorizontal.ReadValue<float>() * Time.deltaTime;

        if (allowRotation)
        {
            Cursor.lockState = CursorLockMode.Locked;
            rotationVelocity += inputActions.Player.Look.ReadValue<Vector2>().x;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }


        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit);

        cameraTech.transform.RotateAround(hit.point, Vector3.up, rotationVelocity);

    }

    private void Update()
    {
        UpdateTimer();
        CheckAllowRotation();
        float deltaZoom = ClampDeltaScroll();

        rotFreeTransform.position = transform.position;
        rotFreeTransform.Rotate(Vector3.up * (transform.eulerAngles.y - rotFreeTransform.eulerAngles.y));

        Vector2 DesiredMovement = inputActions.Player.Move.ReadValue<Vector2>()  * Time.deltaTime * movementMultiplier * (zoomPercentage + 0.7f) * accelerationCurve.Evaluate(accelerationTimer / accelerationTime);

        transform.position += rotFreeTransform.TransformDirection(DesiredMovement.Swizzle3("x0y"));


        transform.position = transform.position.With( y: math.lerp(MinAltitude, MaxAltitude, ZoomAltitudeCurve.Evaluate(zoomPercentage + deltaZoom)));

        transform.Rotate(Vector3.right * (math.lerp(MinRotation, MaxRotation, zoomRotationCurve.Evaluate(zoomPercentage + deltaZoom)) - transform.eulerAngles.x));
        RotateCamera();

        zoomPercentage += deltaZoom;
    }

        Vector2 GetMouseUV()
    {
        // Get the mouse position in screen coordinates
        Vector2 mousePosition = Input.mousePosition;

        // Get the screen dimensions
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Convert mouse position to UV coordinates
        Vector2 uv = new Vector2(
            mousePosition.x / screenWidth,
            mousePosition.y / screenHeight
        );

        return uv;
        //
    }

    Vector2 mouseCorner(Vector2 mouseUV)
    {
        float2 centralizedUV = mouseUV - new Vector2(0.5f,0.5f);
        float2 absoluteUV =  math.abs(centralizedUV);
       float borderSensitivityMap = math.pow(math.max(absoluteUV.x,absoluteUV.y) * 2,20);


       
       return math.clamp( borderSensitivityMap * math.normalize(centralizedUV), new float2(-1.2f,-1.2f),new float2(1.2f, 2));
    }


}