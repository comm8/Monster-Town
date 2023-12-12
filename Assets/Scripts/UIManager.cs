using UnityEngine;
using CameraUtilities;

public class UIManager : MonoBehaviour
{
   [SerializeField] Transform cameraTransform;
    void Update()
    {
        Vector3 Rayhitpoint =
    cameraTransform.TransformDirection(Vector3.forward * CameraUtil.DistanceToPlane(cameraTransform.position, cameraTransform.rotation)) + cameraTransform.position;
        Debug.Log("CURRENT UI" + CameraUtil.PositionToTile(Rayhitpoint));


       // Debug.Log("POSITION" + Rayhitpoint);
    }
}
