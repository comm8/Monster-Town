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

        rotationVelocity = inputActions.Player.Rotate.ReadValue<float>() * Time.deltaTime;

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

        Vector2 DesiredMovement = inputActions.Player.Move.ReadValue<Vector2>() * Time.deltaTime * movementMultiplier * (zoomPercentage + 0.7f) * accelerationCurve.Evaluate(accelerationTimer / accelerationTime);

        transform.position += rotFreeTransform.TransformDirection(Swizzle._x0y(DesiredMovement));


        transform.position = Swizzle.SetY(transform.position, math.lerp(MinAltitude, MaxAltitude, ZoomAltitudeCurve.Evaluate(zoomPercentage + deltaZoom)));

        transform.Rotate(Swizzle._x00(math.lerp(MinRotation, MaxRotation, zoomRotationCurve.Evaluate(zoomPercentage + deltaZoom)) - transform.eulerAngles.x));
        RotateCamera();

        zoomPercentage += deltaZoom;
    }

}