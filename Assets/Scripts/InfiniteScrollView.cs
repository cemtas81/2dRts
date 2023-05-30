using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteScrollView : MonoBehaviour
{
    public RectTransform itemPrefab;
    public RectTransform content;
    public ScrollRect scrollRect;
    public float itemHeight;
    public int poolSize;
    public int startItemCount;

    private List<RectTransform> itemPool;
    private List<RectTransform> activeItems;
    private float contentHeight;
    private float scrollPosition;

    private void Start()
    {
        itemPool = new List<RectTransform>();
        activeItems = new List<RectTransform>();

        contentHeight = startItemCount * itemHeight;

        InitializeItems();
        ResizeContent();
    }

    private void Update()
    {
        scrollPosition = content.anchoredPosition.y;
        UpdateVisibleItems();
    }

    private void InitializeItems()
    {
        for (int i = 0; i < poolSize; i++)
        {
            RectTransform item = Instantiate(itemPrefab, content);
            itemPool.Add(item);
            item.gameObject.SetActive(false);
        }

        for (int i = 0; i < startItemCount; i++)
        {
            ActivateItem(i);
        }
    }

    private void ResizeContent()
    {
        float contentSize = Mathf.Max(startItemCount * itemHeight, scrollRect.viewport.rect.height);
        content.sizeDelta = new Vector2(content.sizeDelta.x, contentSize);
    }

    private void UpdateVisibleItems()
    {
        float startPosition = scrollPosition - scrollRect.viewport.rect.height;
        float endPosition = scrollPosition + scrollRect.viewport.rect.height;

        // Deactivate items that are not within the visible range
        for (int i = activeItems.Count - 1; i >= 0; i--)
        {
            RectTransform item = activeItems[i];
            float itemPosition = item.anchoredPosition.y;

            if (itemPosition + itemHeight < startPosition || itemPosition - itemHeight > endPosition)
            {
                activeItems.RemoveAt(i);
                item.gameObject.SetActive(false);
                itemPool.Add(item);
            }
        }

        // Activate items that are within the visible range
        for (float position = startPosition; position <= endPosition; position += itemHeight)
        {
            if (!IsItemVisible(position))
            {
                ActivateItem(position);
            }
        }
    }

    private void ActivateItem(float position)
    {
        if (itemPool.Count == 0)
            return;

        RectTransform item = itemPool[itemPool.Count - 1];
        itemPool.RemoveAt(itemPool.Count - 1);

        item.gameObject.SetActive(true);
        item.anchoredPosition = new Vector2(item.anchoredPosition.x, position);

        activeItems.Add(item);
    }

    private bool IsItemVisible(float position)
    {
        foreach (RectTransform item in activeItems)
        {
            if (Mathf.Approximately(item.anchoredPosition.y, position))
                return true;
        }

        return false;
    }
}



