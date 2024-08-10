using UnityEngine;
using cmdwtf.UnityTools.Dynamics;
using System.Collections;

public class TileAnimator : MonoBehaviour
{
    [SerializeField] Vector3 blendPos;
    [SerializeField] Vector3 blendScale;
    [SerializeField] DynamicsTransform dynamicsTransform;

    float timeAtClick;
    private void FixedUpdate()
    {
        transform.localScale = Vector3.one;
    }

    public void playUpdateAnimation()
    {
        timeAtClick = Time.time;
        if (!dynamicsTransform.enabled)
        {
            dynamicsTransform.enabled = true;
            enabled = true; StartCoroutine(CheckAllowSleep(3f));
        }

        transform.localScale = blendScale;

    }

    public void CacheDeltaPos()
    {
        StartCoroutine(CheckAllowSleep(1));
    }

    IEnumerator CheckAllowSleep(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (timeAtClick + seconds >= Time.time)
        {
            StartCoroutine(CheckAllowSleep(seconds));
        }
        else
        {
            dynamicsTransform.enabled = false;
            enabled = false;
            transform.localScale = Vector3.one;
        }

    }


}
