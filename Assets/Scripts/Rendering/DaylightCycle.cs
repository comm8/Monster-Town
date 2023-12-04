using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaylightCycle : MonoBehaviour
{
    [SerializeField] Transform sunTransform, moonTransform;
    [SerializeField] float speedMultiplier;

    [SerializeField] Gradient SunColor, moonColor;

    void Awake()
    {
        
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;

        sunTransform.Rotate(new Vector3(speedMultiplier,0,0) * deltaTime);
        moonTransform.Rotate(new Vector3(speedMultiplier,0,0) * deltaTime);

    }
}
