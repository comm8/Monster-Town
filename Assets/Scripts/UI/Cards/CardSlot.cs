using System;
using System.Collections;
using System.Collections.Generic;
using BuildingTools;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Card myCard;
    bool hasCard;

    public void SetCard(Card card)
    {
        if (myCard == null)
        {
            myCard = card;
            card.slot = this;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("pointer enter");
        CardManager.instance.MouseOnSlot(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CardManager.instance.MouseOffSlot(this);
    }

    public void PushCardToSlot(CardSlot slot)
    {
        if (myCard == null) { return; }
        myCard.transform.SetParent(slot.transform);
        myCard.ReleaseCard();

    }

    internal void ReturnCardToSelf()
    {
        if (myCard == null) { return; }
        myCard.transform.SetParent(transform);
        myCard.ReleaseCard();
    }
}
