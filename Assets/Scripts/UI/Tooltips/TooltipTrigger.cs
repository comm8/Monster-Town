using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] string header;
    [TextArea(4, 8)][SerializeField] string content;

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Show(content, header);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }

    void OnMouseEnter()
    {
        TooltipSystem.Show(content, header);
    }

    void OnMouseExit()
    {
        TooltipSystem.Hide();
    }
}
