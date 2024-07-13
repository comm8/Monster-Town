using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    bool dragging;
    public Image image;
    public CardSlot slot;

    Vector3 offset;

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragging = true;
       // offset = 
        CardManager.instance.SetCurrentlyHeldCard(this);
        image.raycastTarget = false;
        //report to card manager;
    }



    public void OnDrag(PointerEventData eventData)
    {
        transform.position += eventData.delta._xy0();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        CardManager.instance.TrySetCardToSlot();

        CardManager.instance.SetCurrentlyHeldCard(null);
        image.raycastTarget = true;
        transform.SetParent(slot.transform);
        ReleaseCard();

    }

    public void ReleaseCard()
    {
        dragging = false;
        LeanTween.moveLocal(gameObject, Vector3.zero, 0.5f).setEase(LeanTweenType.easeOutExpo);
    }

    void Start()
    {

    }
}
