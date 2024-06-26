using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler,IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
       
    }

    public void OnDrag(PointerEventData eventData)
    {
      transform.position += eventData.delta._xy0();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        LeanTween.moveLocal(gameObject, Vector3.zero, 0.5f).setEase(LeanTweenType.easeOutExpo);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
