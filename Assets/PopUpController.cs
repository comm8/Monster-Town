using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpController : MonoBehaviour
{
    public RectTransform rectTransform;
    public Transform transformTarget;
    public Transform originalTransform;

    public float width;
    public float height;
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        rectTransform.sizeDelta = new Vector2(transformTarget.localScale.x, transformTarget.localScale.y);
        originalTransform.localScale = new Vector3(width, height, 0);
    }
}
