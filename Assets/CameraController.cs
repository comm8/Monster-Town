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
    float zoompercent;

    [SerializeField] Vector3 StartOffset;



    // Start is called before the first frame update
    void Awake()
    {
        inputActions = new Inputactions3D();
        inputActions.Player.Enable();
        zoompercent = .5f;
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

    void UpdateZoom()
    {
        zoompercent += inputActions.Player.Scroll.ReadValue<float>();
        math.clamp(zoom, 0f, alti);
    }

    private void Update()
    {
        UpdateTimer();
        UpdateZoom();

        transform.position.Set(transform.position.x, zoomAlti.Evaluate(inputActions.Player.Scroll.ReadValue<float>()) * alti, transform.position.z );
   

        Vector2 move = math.normalize(inputActions.Player.Move.ReadValue<Vector2>()) * speedMult * curve.Evaluate(timer/ AccelTime);
        transform.position += new Vector3(move.x,0,move.y) * Time.deltaTime;
    }


    

}