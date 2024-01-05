using UnityEngine;
using cmdwtf.UnityTools.Dynamics;
public class TileAnimator : MonoBehaviour
{
    [SerializeField] Vector3 blendPos;
    [SerializeField] Vector3 blendScale;
    [SerializeField] DynamicsTransform dynamicsTransform;

    bool isPressed;

    private void FixedUpdate()
    {
        if (isPressed)
        {
            transform.localScale = Vector3.one;
            isPressed = false;
            enabled = false;
        }


    }

    public void playUpdateAnimation()
    {
        this.enabled = true;
        isPressed = true;
        transform.localScale = blendScale;
    }


}
