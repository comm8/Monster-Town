using UnityEngine;
using Unity.Mathematics;

namespace CameraUtilities
{
    public static class CameraUtil
    {

        public static int2 PositionToTile(Vector3 InputPos)
        {
            return new((int)(( InputPos.x + 5) / 10), (int)((InputPos.z + 5) / 10));
        }

    }
}

