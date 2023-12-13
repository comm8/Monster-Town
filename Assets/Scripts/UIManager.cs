using UnityEngine;
using CameraUtilities;

public class UIManager : MonoBehaviour
{
   [SerializeField] Transform cameraTransform;
    void Update()
    {
       Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        Debug.Log("CURRENT UI" + CameraUtil.PositionToTile(hit.point));
    }
}
