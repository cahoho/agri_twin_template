using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FloatBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltipPrefab;
    public Vector2 offset;
    public float scaleSpeed = 10f;

    private GameObject currentTooltipInstance;
    private bool onCursor;

    private Vector3 originalScale; // 原始缩放
    private Vector3 targetScale;   // 目标缩放

    private void Awake()
    {
        GetComponent<Image>().raycastTarget = true;
        originalScale = transform.localScale;
        targetScale = originalScale; 
    }

    void Update()
    {
        if (onCursor && currentTooltipInstance != null)
        {
            UpdateTooltipPosition();
        }

        if (transform.localScale != targetScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);

            if (Vector3.Distance(transform.localScale, targetScale) < 0.001f)
            {
                transform.localScale = targetScale;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowTooltip();
        targetScale = originalScale * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideTooltip();
        targetScale = originalScale;
    }

    void OnDisable()
    {
        HideTooltip();
        transform.localScale = originalScale;
        targetScale = originalScale;
    }

    void OnDestroy()
    {
        if (currentTooltipInstance != null)
        {
            Destroy(currentTooltipInstance);
        }
    }

    void UpdateTooltipPosition()
    {
        if (currentTooltipInstance == null) return;
        Vector2 mousePos = Input.mousePosition;

        RectTransform barFillRect = GetComponent<RectTransform>();
        Vector2 barFillLocalPoint;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(barFillRect, mousePos, null, out barFillLocalPoint);

        RectTransform tooltipRect = currentTooltipInstance.GetComponent<RectTransform>();
        if (tooltipRect != null)
        {
            tooltipRect.localPosition = barFillLocalPoint + offset;
        }
    }

    void ShowTooltip()
    {
        Debug.Log("Show Tooltip");
        if (currentTooltipInstance == null)
        {
            currentTooltipInstance = Instantiate(tooltipPrefab, gameObject.transform.parent);
            currentTooltipInstance.SetActive(true);
        }
        else
        {
            currentTooltipInstance.SetActive(true);
        }
        onCursor = true;
    }

    void HideTooltip()
    {
        Debug.Log("Hide Tooltip");
        onCursor = false;
        if (currentTooltipInstance != null)
        {
            currentTooltipInstance.SetActive(false);
        }
    }
}
