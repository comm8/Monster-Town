using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPEffects.TMPAnimations.Animations;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{

    public TextMeshProUGUI headerField, contentField;

    public LayoutElement layoutElement;

    [SerializeField] byte characterWrapLimit;

    [SerializeField] RectTransform rectTransform;

    void Update()
    {
        if (Application.isEditor)
        {
            CheckWrapLimit();
        }
        var position = Mouse.current.position.ReadValue();
        transform.position = position;

        UpdatePivot();
        //

        //float pivotX = position.x / Screen.width;
        //float pivotY = position.y / Screen.height;

        // rectTransform.pivot = new Vector2(pivotX, pivotY);

    }

    void UpdatePivot()
    {
        // Get the width of the rect in screen coordinates
        Vector3[] worldCorners = new Vector3[4];
        rectTransform.GetWorldCorners(worldCorners);
        float rectWidth = Vector3.Distance(worldCorners[0], worldCorners[3]);
        float rectHeight = Vector3.Distance(worldCorners[1], worldCorners[0]);

        // Calculate the offset based on the pivot
        rectWidth *= 1.05f;
        rectHeight *= 1.05f;

        Vector2 newPivot = Vector2.zero;

        // Check if the mouse position plus offset is offscreen to the right
        if (Input.mousePosition.x + rectWidth > Screen.width)
        {
            newPivot.x = 1.05f;
        }
        else
        {
            newPivot.x = -0.05f;
        }

        if (Input.mousePosition.y + rectHeight > Screen.height)
        {
            newPivot.y = 1.05f;
        }
        else
        {
            newPivot.y = -0.05f;
        }

        rectTransform.pivot = newPivot;



    }

    public void CheckWrapLimit()
    {
        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
    }

    public void SetText(string content, string header = "")
    {
        if (string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
        }
        else
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }

        contentField.text = content;

        CheckWrapLimit();
    }

}
