using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationPopup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.moveLocalY(this.gameObject, 0, 0.4f).setEase(LeanTweenType.easeOutBounce);
    }

    public void RemovePopup()
    {
       LeanTween.moveLocalY(this.gameObject, 14, 1.6f).setEase(LeanTweenType.punch);
        LeanTween.scale(this.gameObject, Vector3.zero, 0.4f).setEase(LeanTweenType.easeInExpo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
