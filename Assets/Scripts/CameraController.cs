using Unity.Mathematics;
using UnityEngine;
using CameraUtilities;


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


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawSphere(new(transform.position.x, MinAltitude, transform.position.x) , 1);
       // Gizmos.DrawSphere(new(transform.position.x, MaxAltitude, transform.position.x), 1);


        Vector3 Rayhitpoint =
  transform.TransformDirection(Vector3.forward * CameraUtil.DistanceToPlane(transform.position, transform.rotation)) + transform.position;
        Gizmos.DrawSphere(Rayhitpoint, 1);
    }

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
        if (inputActions.Player.Move.ReadValue<Vector2>().magnitude > Mathf.Epsilon)
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
        allowRotation = (inputActions.Player.RotationMode.ReadValue<float>() > .02f || usingController);
    }

    private float ClampDeltaScroll()
    {
        float deltaScroll = inputActions.Player.Scroll.ReadValue<float>();

        if (allowRotation)
        {
            deltaScroll += inputActions.Player.Look.ReadValue<Vector2>().y;
        }


        if (deltaScroll + zoomPercentage > 1)
        {
            deltaScroll = 1 - zoomPercentage;
        }
        else if (deltaScroll + zoomPercentage < 0)
        {
            deltaScroll = -zoomPercentage;
        }


        return deltaScroll;
    }

    private void RotateCamera()
    {

        rotationVelocity = 0.0f;
        float angleA = 90.0f - transform.eulerAngles.x;
        float hypotenuse = transform.position.y / math.cos(math.radians(angleA));

        Vector3 Rayhitpoint = transform.TransformDirection(Vector3.forward * hypotenuse) + transform.position;

        if (allowRotation)
        {
            Cursor.lockState = CursorLockMode.Locked;
            rotationVelocity += inputActions.Player.Look.ReadValue<Vector2>().x;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        cameraTech.transform.RotateAround(Rayhitpoint, Vector3.up, rotationVelocity);

    }

    private void Update()
    {
        UpdateTimer();
        CheckAllowRotation();
        float deltaScroll = ClampDeltaScroll();



        rotFreeTransform.position = transform.position;
        rotFreeTransform.Rotate(Vector3.up * (transform.eulerAngles.y - rotFreeTransform.eulerAngles.y));
        Vector2 DesiredMovement = inputActions.Player.Move.ReadValue<Vector2>() * Time.deltaTime * movementMultiplier * (zoomPercentage + 0.7f) * accelerationCurve.Evaluate(accelerationTimer / accelerationTime);


        transform.position += rotFreeTransform.TransformDirection(new Vector3(DesiredMovement.x, 0, DesiredMovement.y));
        transform.position = new(transform.position.x, math.lerp(MinAltitude, MaxAltitude, ZoomAltitudeCurve.Evaluate(zoomPercentage + deltaScroll)), transform.position.z);

        transform.Rotate(new(math.lerp(MinRotation, MaxRotation, zoomRotationCurve.Evaluate(zoomPercentage + deltaScroll)) - transform.eulerAngles.x, 0.0f, 0.0f));
        RotateCamera();

        zoomPercentage += deltaScroll;
    }
}