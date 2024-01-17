using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupRoad : MonoBehaviour
{

    [SerializeField] float squareSize;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void checkRoadDirectionDesired(Vector2 UV)
    {
        if ((UV.x > 0.5 - squareSize && UV.x < 0.5 + squareSize) && (UV.y > 0.5 - squareSize && UV.y < 0.5 + squareSize))
        {
            //we are in the center square
            return;
        }

        if (1 > UV.x / UV.y)
        {
            //we are in the bottom left
            return;
        }

        if(-1 > (UV.x - 1) / UV.y)
        {
            //we are in the top right quad
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
