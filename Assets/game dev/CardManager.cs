using Unity.Mathematics;
using UnityEngine;
using SerializableDictionary;
using System.Collections.Generic;
using System;

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
    void Awake()
    {
        instance = this;
    }



    public List<UITransformContainer> UITransformContainers;

    Vector3 scale = Vector3.one * 75;



    public void addUItransform(UITransformContainer container)
    {
        UITransformContainers.Add(container);
    }

    void LateUpdate()
    {
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
    }



//Cards: 
//every unit has a card in the card panel, which can be dragged out and released on a card slot
//when released, the card gets parented to the valid slot, and leantweens to the correct pos and scale.
//if theres no new valid spot, it returns via leantween to its original pos.


}