using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class NotificationPopup : MonoBehaviour
{

    Vector3 lastPos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.moveLocalY(this.gameObject, 0, 0.4f).setEase(LeanTweenType.easeOutBounce);
    }

    public void RemovePopup()
    {
        LeanTween.moveLocalY(this.gameObject, 14, 1.6f).setEase(LeanTweenType.punch);
        LeanTween.scale(this.gameObject, Vector3.zero, 0.4f).setEase(LeanTweenType.easeInExpo).setOnComplete(DestroyPopup);
    }

    void DestroyPopup()
    {
        Destroy(transform.parent.gameObject);
    }

    void Update()
    {
        //transform.position += (transform.position - lastPos) * 0.1f;
        //lastPos = transform.parent.position;

    }
}
