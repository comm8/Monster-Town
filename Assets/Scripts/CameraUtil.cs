using UnityEngine;
using Unity.Mathematics;

namespace CameraUtilities
{
    public static class CameraUtil
    {
        public static float DistanceToPlane(Vector3 InputPosition, Quaternion Dir)
        {
            float angleA = 90.0f - Dir.eulerAngles.x;
            return InputPosition.y / math.cos(math.radians(angleA));
        }

        public static int2 PositionToTile(Vector3 InputPos)
        {
            return new((int)(( InputPos.x + 5) / 10), (int)((InputPos.z + 5) / 10));
        }

    }
}

