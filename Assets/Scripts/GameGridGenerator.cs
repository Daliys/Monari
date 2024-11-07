using System.Collections.Generic;
using UnityEngine;

public class GameGridGenerator : MonoBehaviour
{
    [SerializeField] private GameObject gridItemPrefab;
    [SerializeField] private float itemsSpacing;
    [SerializeField] private float horizontalPadding;
    [SerializeField] private float verticalPadding;


    public List<GridItem> GenerateItems(int rows, int columns)
    {
        List<GridItem> gridItems = new();
        // Cache RectTransform component
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 gridParentSize = rectTransform.sizeDelta;

        // Pre-calculate paddings and spacing
        float availableWidth = gridParentSize.x - horizontalPadding - (columns - 1) * itemsSpacing;
        float availableHeight = gridParentSize.y - verticalPadding - (rows - 1) * itemsSpacing;
        Vector2 itemSize = new Vector2(availableWidth / columns, availableHeight / rows);

        float itemSizeMin = Mathf.Min(itemSize.x, itemSize.y);
        Vector2 itemOffset = new Vector2(itemSizeMin / 2 + horizontalPadding / 2, itemSizeMin / 2 + verticalPadding / 2);
        float itemSpacingOffset = itemSizeMin + itemsSpacing;

        // Create grid items
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject gridItem = Instantiate(gridItemPrefab, transform);
                RectTransform itemRectTransform = gridItem.GetComponent<RectTransform>();
                itemRectTransform.sizeDelta = new Vector2(itemSizeMin, itemSizeMin);
                itemRectTransform.anchoredPosition = new Vector2(j * itemSpacingOffset + itemOffset.x, -i * itemSpacingOffset - itemOffset.y);
                gridItems.Add(gridItem.GetComponent<GridItem>());
            }
        }

        // Adjust parent grid size
        rectTransform.sizeDelta = new Vector2(columns * itemSpacingOffset - itemsSpacing + horizontalPadding,
            rows * itemSpacingOffset - itemsSpacing + verticalPadding
        );

        return gridItems;
    }
    
}
