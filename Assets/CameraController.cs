using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] float speedMult;
    Inputactions3D inputActions;


    [SerializeField] AnimationCurve curve;
    [SerializeField] float AccelTime;
     float timer;

    [SerializeField] float zoom;
    [SerializeField] AnimationCurve zoomRot;
    [SerializeField] float rot;
    [SerializeField] AnimationCurve zoomAlti;
    [SerializeField] float alti;
    [SerializeField] float scrollmult;

    [SerializeField] Vector3 localOffset;
    [SerializeField] Vector3 localRotation;





    // Start is called before the first frame update
    void Awake()
    {
        inputActions = new Inputactions3D();
        inputActions.Player.Enable();
        zoom = .5f;
    }
    void OnDestroy()
    {
        inputActions.Dispose();
    }


    private void UpdateTimer()
    {
        if (inputActions.Player.Move.ReadValue<Vector2>().magnitude > 0.1f)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }
    }


    private void Update()
    {
        UpdateTimer();

        //localRotation = new Vector3(0, (zoomRot.Evaluate(zoom) * alti), 0);
        //transform.rotation = Quaternion.Euler(localRotation - transform.rotation.eulerAngles);

        float deltaScroll =  inputActions.Player.Scroll.ReadValue<float>() * scrollmult * Time.deltaTime;


        Vector2 move = math.normalize(inputActions.Player.Move.ReadValue<Vector2>()) * Time.deltaTime * speedMult * curve.Evaluate(timer/ AccelTime);
     
        transform.position += new Vector3(move.x, 0, move.y);
        transform.position += new Vector3(0, (zoomAlti.Evaluate(zoom + deltaScroll) - zoomAlti.Evaluate(zoom)) * alti, 0);

        //transform.position += new Vector3(0, zoomRot.Evaluate(zoom + deltaScroll), 0);


        zoom += deltaScroll;
        zoom = math.clamp(zoom, 0f, 1f);
    }


    

}