using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

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

    bool RotationDesired;
    bool rotating;

    [Header("Mouse sprites")]
    [SerializeField] Texture2D mouseDefault;
    [SerializeField] Texture2D mousePan;


    void Awake()
    {

        Cursor.lockState = CursorLockMode.Confined;
        inputActions = new Inputactions3D();
        inputActions.Player.Enable();

    }
    void OnDestroy()
    {
        inputActions.Dispose();
    }



    private void AcceleratePan()
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
        RotationDesired = (inputActions.Player.RotationMode.ReadValue<float>() > Mathf.Epsilon || usingController);
    }

    private float ClampDeltaScroll()
    {
        float deltaScroll = 0;

        if (!GameManager.instance.pointerOverUI) { deltaScroll = inputActions.Player.Scroll.ReadValue<float>(); }
        if (RotationDesired) { deltaScroll += inputActions.Player.Look.ReadValue<Vector2>().y; }


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

    private void SetRotateMode()
    {
        CheckAllowRotation();
        if (RotationDesired == rotating) //Doesn't need mode change; Update rotation.
        {
            if (RotationDesired) { UpdateRotateCamera(); }
            else { return; }
        }
        else //We need to change current mode.
        {
            rotating = RotationDesired;
            if (RotationDesired) { StartRotateCamera(); }
            else { EndRotateCamera(); }
        }
    }


    private void StartRotateCamera()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.SetCursor(mousePan, Vector2.zero, CursorMode.Auto);
        UpdateRotateCamera();
    }
    private void EndRotateCamera()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.SetCursor(mouseDefault, Vector2.zero, CursorMode.Auto);
    }
    private void UpdateRotateCamera()
    {

        rotationVelocity = inputActions.Player.Rotate.ReadValue<float>() + inputActions.Player.Look.ReadValue<Vector2>().x;

        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit);

        cameraTech.transform.RotateAround(hit.point, Vector3.up, rotationVelocity);

    }

    private void Update()
    {
        AcceleratePan();
        float deltaScroll = ClampDeltaScroll();


        rotFreeTransform.position = transform.position;
        rotFreeTransform.Rotate(Vector3.up * (transform.eulerAngles.y - rotFreeTransform.eulerAngles.y));
        Vector2 DesiredMovement = inputActions.Player.Move.ReadValue<Vector2>() * Time.deltaTime * movementMultiplier * (zoomPercentage + 0.7f) * accelerationCurve.Evaluate(accelerationTimer / accelerationTime);

        transform.position += rotFreeTransform.TransformDirection(new Vector3(DesiredMovement.x, 0, DesiredMovement.y));
        transform.position = new(transform.position.x, math.lerp(MinAltitude, MaxAltitude, ZoomAltitudeCurve.Evaluate(zoomPercentage + deltaScroll)), transform.position.z);

        transform.Rotate(new(math.lerp(MinRotation, MaxRotation, zoomRotationCurve.Evaluate(zoomPercentage + deltaScroll)) - transform.eulerAngles.x, 0.0f, 0.0f));
        SetRotateMode();

        zoomPercentage += deltaScroll;
    }
}