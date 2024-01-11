using UnityEngine;
using cmdwtf.UnityTools.Dynamics;
using System.Collections;

public class TileAnimator : MonoBehaviour
{
    [SerializeField] Vector3 blendPos;
    [SerializeField] Vector3 blendScale;
    [SerializeField] DynamicsTransform dynamicsTransform;

    float timeAtClick;
    

    public void playUpdateAnimation()
    {
        dynamicsTransform.enabled = true;
        transform.localScale = blendScale;
        StartCoroutine(press());
    }

    public void CacheDeltaPos()
    {
        StartCoroutine(DisableDyamicTransform(0.2f));
    }

    IEnumerator press()
    {
        timeAtClick = Time.time;
        yield return new WaitForFixedUpdate();
        transform.localScale = Vector3.one;
        StartCoroutine(DisableDyamicTransform(3));
    }

    IEnumerator DisableDyamicTransform(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (timeAtClick + seconds + 0.05 < Time.time)
        {
            StartCoroutine(DisableDyamicTransform(seconds));
        }
        else
        {
        transform.localScale = Vector3.one;
        dynamicsTransform.enabled = false;
        }    

    }

}
