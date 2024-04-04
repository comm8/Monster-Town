using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem instance;

    public void Awake()
    {
        instance = this;
    }

    public Tooltip tooltip;

    public static void Show(string content, string header = "")
    {
        instance.tooltip.SetText(content, header);
        instance.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        instance.tooltip.gameObject.SetActive(false);
    }
}
