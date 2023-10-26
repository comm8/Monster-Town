using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] float zoomFullRotationAmount;
    [SerializeField] AnimationCurve ZoomAltitudeCurve;
    [SerializeField] float zoomPeakAltitude;
    [SerializeField] float scrollWheelMultiplier;


    [SerializeField] float rotateSpeed;

   [SerializeField] Transform rotFreeTransform;




    // Start is called before the first frame update
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
        if (inputActions.Player.Move.ReadValue<Vector2>().magnitude > 0.1f)
        {
            accelerationTimer += Time.deltaTime;
        }
        else
        {
            accelerationTimer = 0;
        }
    }

    private float ClampDeltaScroll()
    {
        float deltaScroll = inputActions.Player.Scroll.ReadValue<float>() * scrollWheelMultiplier * Time.deltaTime;
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
        if (inputActions.Player.RotationMode.ReadValue<float>() > .5f)
        {
            Vector3 Rayhitpoint;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;


            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 500))
            {
                Rayhitpoint = hit.point;
            }
            else
            {
                float hypotenuse = transform.position.y / -math.cos(transform.eulerAngles.x) ;
                Debug.Log(transform.position.y);
                Debug.Log(-math.cos(transform.eulerAngles.x));
                Debug.Log(hypotenuse);

                Rayhitpoint = transform.TransformDirection(Vector3.forward * hypotenuse);
            }

            transform.RotateAround(Rayhitpoint, Vector3.up, inputActions.Player.Look.ReadValue<Vector2>().x * Time.deltaTime * rotateSpeed);
        }
        else
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }
    }

    private void Update()
    {
        UpdateTimer();
       float deltaScroll = ClampDeltaScroll();


        rotFreeTransform.position = transform.position;
        rotFreeTransform.Rotate(Vector3.up*(transform.eulerAngles.y - rotFreeTransform.eulerAngles.y));
        Vector2 DesiredMovement =  inputActions.Player.Move.ReadValue<Vector2>() * Time.deltaTime * movementMultiplier * (zoomPercentage + 0.7f) * accelerationCurve.Evaluate(accelerationTimer/ accelerationTime);


        transform.position += rotFreeTransform.TransformDirection(new Vector3(DesiredMovement.x, (ZoomAltitudeCurve.Evaluate(zoomPercentage + deltaScroll) - ZoomAltitudeCurve.Evaluate(zoomPercentage)) * zoomPeakAltitude, DesiredMovement.y));

        transform.Rotate((zoomRotationCurve.Evaluate(zoomPercentage + deltaScroll) - zoomRotationCurve.Evaluate(zoomPercentage)) * zoomFullRotationAmount, 0, 0);
        RotateCamera();

        zoomPercentage += deltaScroll;
    }
}