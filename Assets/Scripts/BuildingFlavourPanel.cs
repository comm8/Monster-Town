using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFlavourPanel : MonoBehaviour
{

    [SerializeField] bool startExtended;
    [SerializeField] float pullTabExtendLength = 17f;
    [SerializeField] float PanelExtendLength = 105f;



    bool toggled = true;
    [SerializeField] GameObject pullTab;

    void Start()
    {
        if(!startExtended)
        {
            toggled=false;
            pullTab.transform.localPosition += transform.up * pullTabExtendLength;
            transform.localPosition += transform.up * PanelExtendLength;
        }
    }


    public void Toggle()
    {
        toggled = !toggled;
        LeanTween.moveLocalY(pullTab, pullTab.transform.localPosition.y + (pullTabExtendLength * (toggled ? -1 : 1)), 0.5f).setEase(LeanTweenType.easeOutBounce).setOnComplete(TogglePanel);
    }

    void TogglePanel()
    {
        LeanTween.moveLocalY(gameObject, gameObject.transform.localPosition.y + (PanelExtendLength * (toggled ? -1 : 1)) , 0.5f).setEase(LeanTweenType.easeOutBounce);
    }
}
