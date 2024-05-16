using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using BuildingTools;
using Unity.Mathematics;

public class DayNightCycle : MonoBehaviour
{
    [HideInInspector] public static DayNightCycle instance; 

    [SerializeField] Transform sunTransform, moonTransform;
    [SerializeField] Gradient SunColor, moonColor;


    [SerializeField] float speedMultiplier;
    [SerializeField] TMP_Text TextMesh;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    public  void UpdateDayNightCycle()
    {
        float deltaTime = Time.deltaTime;

        sunTransform.Rotate(Vector3.right * (speedMultiplier * deltaTime * 360));
        moonTransform.Rotate(Vector3.right * (speedMultiplier * deltaTime * 360));

        //RenderSettings.ambientIntensity = math.sin(Time.time * speedMultiplier);

        //
        int curtime = (int)(Time.time / 2);
        TextMesh.text = curtime / 6 + ":" + (curtime % 6) + "0";
    }
}
