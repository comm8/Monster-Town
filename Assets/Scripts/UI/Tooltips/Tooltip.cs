using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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



            //float pivotX = position.x / Screen.width;
            //float pivotY = position.y / Screen.height;

           // rectTransform.pivot = new Vector2(pivotX, pivotY);
        
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
