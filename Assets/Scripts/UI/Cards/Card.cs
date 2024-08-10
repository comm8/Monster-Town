using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    public Image image;
    public CardSlot slot;

    Vector3 offset;

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - new Vector3(eventData.position.x, eventData.position.y, 0);
        CardManager.instance.SetCurrentlyHeldCard(this);
        image.raycastTarget = false;
        //report to card manager;
    }



    public void OnDrag(PointerEventData eventData)
    {
        transform.position = new Vector3(eventData.position.x, eventData.position.y, 0) + offset;
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
        LeanTween.cancel(gameObject);
        image.raycastTarget = false;
        LeanTween.moveLocal(gameObject, Vector3.zero, 0.5f).setEase(LeanTweenType.easeOutExpo).setOnComplete(UpdateCollision);
        LeanTween.scale(gameObject, Vector3.one, 0.5f).setEase(LeanTweenType.easeOutExpo);
    }

    void UpdateCollision()
    {
        image.raycastTarget = true;
    }
}
