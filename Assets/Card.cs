using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    bool dragging;

    public CardSlot slot;

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragging = true;
        //report to card manager;
    }



    public void OnDrag(PointerEventData eventData)
    {

        transform.position += eventData.delta._xy0();
        //check for slot below



    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ReleaseCard();
    }

    public void ReleaseCard()
    {
        dragging = false;
        transform.eulerAngles = new(0, 0, 0);
        LeanTween.moveLocal(gameObject, Vector3.zero, 0.5f).setEase(LeanTweenType.easeOutExpo);
    }

    void Start()
    {

    }
}
