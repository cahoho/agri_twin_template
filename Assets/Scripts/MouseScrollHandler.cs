using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ScrollRect))]
public class MouseScrollHandler : MonoBehaviour, IScrollHandler
{
    private ScrollRect scrollRect;
    private RectTransform content;
    private ContentSizeFitter sizeFitter;

    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        content = scrollRect.content;
        sizeFitter = content.GetComponent<ContentSizeFitter>();

        // 延迟一帧确保布局计算完成
        Invoke("RefreshScrollView", 0.1f);
    }

    void RefreshScrollView()
    {
        // 强制刷新布局
        LayoutRebuilder.ForceRebuildLayoutImmediate(content);

        // 重置滚动位置到开始
        scrollRect.verticalNormalizedPosition = 1f;
        scrollRect.horizontalNormalizedPosition = 0f;
    }

    public void OnScroll(PointerEventData eventData)
    {
        if (scrollRect.vertical && Mathf.Abs(eventData.scrollDelta.y) > 0)
        {
            scrollRect.verticalNormalizedPosition += eventData.scrollDelta.y * scrollRect.scrollSensitivity * 0.001f;
        }

        if (scrollRect.horizontal && Mathf.Abs(eventData.scrollDelta.x) > 0)
        {
            scrollRect.horizontalNormalizedPosition += eventData.scrollDelta.x * scrollRect.scrollSensitivity * 0.001f;
        }
    }
}