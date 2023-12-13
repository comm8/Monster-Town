using UnityEngine;
using Unity.Mathematics;

namespace MonstroCity
{
    public static class MonstroUtil
    {

        public static int2 PositionToTile(Vector3 InputPos)
        {
            return new((int)(( InputPos.x + 5) / 10), (int)((InputPos.z + 5) / 10));
        }

        

    }
}

