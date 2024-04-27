using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFlavourPanel : MonoBehaviour
{
    bool toggle = true;
    public GameObject pullTab;


    public void Toggle()
    {
        toggle = !toggle;
        if (!toggle)
        {
            LeanTween.moveLocalY(pullTab, -50f, 0.7f).setEase(LeanTweenType.easeOutBounce).setOnComplete(PullInPanel);
        }
        else
        {
            LeanTween.moveLocalY(pullTab, -67f, 0.7f).setEase(LeanTweenType.easeOutBounce).setOnComplete(PullOutPanel);
        }
    }

    void PullInPanel()
    {
        LeanTween.moveLocalY(gameObject, 200, 0.5f).setEase(LeanTweenType.easeOutBounce);
    }
    void PullOutPanel()
    {
        LeanTween.moveLocalY(gameObject, 95, 0.5f).setEase(LeanTweenType.easeOutBounce);
    }

}
