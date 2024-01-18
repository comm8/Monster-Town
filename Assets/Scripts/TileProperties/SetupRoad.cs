using Unity.Mathematics;
using UnityEngine;
using SerializableDictionary.Scripts;

public class SetupRoad : MonoBehaviour
{

    [SerializeField] float squareSize;
      [SerializeField] SerializableDictionary<AdjustedBoolTable, Vector2> roadShapeDictionary;

        AdjustedBoolTable adjustedBools;

    void checkRoadDirectionDesired(Vector2 UV)
    {
    RoadBoolTable roadBools = new RoadBoolTable();
        if ((UV.x > 0.5 - squareSize && UV.x < 0.5 + squareSize) && (UV.y > 0.5 - squareSize && UV.y < 0.5 + squareSize))
        {
            roadBools.center = true;
            return;
        }

        if (1 > UV.x / UV.y)
        {
            roadBools.topleft = true;
            //we are in the top left
        }
        else
        {
            roadBools.bottomright = true;
            //we are in the bottom right
        }

        if(-1 > (UV.x - 1) / UV.y)
        {
            roadBools.bottomleft = true;
            //we are in the bottom left
        }
        else
        {
            roadBools.topright = false;
            //we are in the top right
        }

        combo(roadBools, adjustedBools);

        UV = roadShapeDictionary.Get(adjustedBools);
    }

    void combo(RoadBoolTable road, AdjustedBoolTable adjustedRoad)
    {
            adjustedRoad.top = (road.topleft && road.topright);
            adjustedRoad.bottom = (road.bottomleft && road.bottomright);
            adjustedRoad.right = (road.topright && road.bottomright);
            adjustedRoad.left = (road.topleft && road.bottomleft);
    }

    public class RoadBoolTable
    {
        public bool topleft, topright, bottomleft, bottomright, center;
    }

    public class AdjustedBoolTable
    {
        public bool left, right, bottom, top, center;
    }

    void updateRoadTex()
    {

    }

}
