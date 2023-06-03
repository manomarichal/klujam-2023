using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonRunAway : MonoBehaviour
{
    public RectTransform buttonRectTransform;
    public Vector2 hoverOffset;

    private Vector2 originalPosition;

    private void Awake()
    {
        originalPosition = buttonRectTransform.anchoredPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonRectTransform.anchoredPosition = originalPosition + hoverOffset;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonRectTransform.anchoredPosition = originalPosition;
    }
}
