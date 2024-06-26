using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    public Transform myCard;
    bool hasCard;

    void Update()
    {
        if(myCard)
        {
         myCard.position = transform.position;   
        }
    }
}
