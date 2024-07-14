using Unity.Mathematics;
using UnityEngine;
using SerializableDictionary;
using System.Collections.Generic;
using System;
using UnityEngine.InputSystem;

public class CardManager : MonoBehaviour
{

    public static CardManager instance;
    [Serializable]
    public struct UITransformContainer
    {
        public Transform plane;

        public RectTransform uiElement;



        public UITransformContainer(Transform Plane, RectTransform UIElement)
        {
            plane = Plane; uiElement = UIElement;
        }
    }

    public GameObject cardSlotTemplate, UIParent;
    Transform cameraTransform;

    [SerializeField] Card heldCard;
    [SerializeField] CardSlot selectedSlot;
    void Awake()
    {
        instance = this;
    }



    public List<UITransformContainer> UITransformContainers;

    Vector3 scale = Vector3.one * 75;


    public void CreateSlot(Transform parent3DEntity, bool CreateCard)
    {
        var CardSlot = Instantiate(cardSlotTemplate, UIParent.transform);

        addUItransform(new UITransformContainer(parent3DEntity, CardSlot.GetComponent<RectTransform>()));

    }
    public void CreateCard()
    {

    }

    /// <summary>
    /// Adds A UI element synced to world space
    /// </summary>
    public void addUItransform(UITransformContainer container)
    {
        UITransformContainers.Add(container);
    }

    void LateUpdate()
    {
        if (UIParent.activeSelf == false) { return; }
        foreach (var container in UITransformContainers)
        {
            AlignUIElementWithPlane(container.plane, container.uiElement);
        }

    }

    void AlignUIElementWithPlane(Transform plane, RectTransform uiElement)
    {
        Vector3 planeScreenPosition = Camera.main.WorldToScreenPoint(plane.position);
        uiElement.localScale = scale / Vector3.Project(plane.position - Camera.main.transform.position, Camera.main.transform.forward).magnitude;
        uiElement.position = planeScreenPosition;

        //somehow sort the UI elements based on depth
    }

    public void MouseOnSlot(CardSlot slot)
    {
        if (heldCard != null && heldCard.slot != slot)
        {
            if (selectedSlot != null)
            {
                MouseOffSlot(selectedSlot);
            }
            selectedSlot = slot;
            slot.PushCardToSlot(heldCard.slot);
        }
    }

    internal void MouseOffSlot(CardSlot slot)
    {


        if (slot == selectedSlot) { selectedSlot = null; }


        if (heldCard != null)
        {
            if (slot == heldCard.slot) { return; }
            slot.ReturnCardToSelf();
        }
    }


    public void SetCurrentlyHeldCard(Card card)
    {
        heldCard = card;
    }

    internal void TrySetCardToSlot()
    {
        if (selectedSlot != null)
        {
            Card tempCard = selectedSlot.myCard;

            selectedSlot.myCard = heldCard;
            tempCard.slot = heldCard.slot;
            heldCard.slot = selectedSlot;
            tempCard.ReleaseCard();
        }




    }

    public void ToggleDisplayCards()
    {
        UIParent.SetActive(!UIParent.activeSelf);
    }

    //Cards: 
    //every unit has a card in the card panel, which can be dragged out and released on a card slot
    //when released, the card gets parented to the valid slot, and leantweens to the correct pos and scale.
    //if theres no new valid spot, it returns via leantween to its original pos.


}