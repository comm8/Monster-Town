using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BuildingUnitWidget : MonoBehaviour
{
    [SerializeField] Transform myTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       myTransform.forward = Camera.main.transform.forward;
    }
}
