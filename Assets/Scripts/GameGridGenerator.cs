using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Generates a grid of items based on the specified rows, columns, and item data.
/// </summary> 
public class GameGridGenerator : MonoBehaviour
{
    [SerializeField] private GameObject gridItemPrefab;
    [SerializeField] private float itemsSpacing;
    [SerializeField] private float horizontalPadding;
    [SerializeField] private float verticalPadding;

    // Pre-calculated values
    private float itemSpacingOffset;
    private Vector2 itemOffset;
    float itemSizeMin;


    /// <summary>
    /// Generates a grid of items based on the specified rows, columns, and item data.
    /// </summary>
    /// <param name="rows">The number of rows in the grid.</param>
    /// <param name="columns">The number of columns in the grid.</param>
    /// <param name="itemData">The list of item data to generate the grid items from.</param>
    public List<GridItem> GenerateItemsWith(int rows, int columns, List<ItemData> itemData)
    {
        List<GridItem> gridItems = new();
        RectTransform rectTransform = GetComponent<RectTransform>();
        PreCalculate(rectTransform.sizeDelta, rows, columns);

        int counter = 0;

        foreach (ItemData item in itemData)
        {
            Vector2Int pos = item.gridPosition;
            GameObject gridItem = Instantiate(gridItemPrefab, transform);
            RectTransform itemRectTransform = gridItem.GetComponent<RectTransform>();
            itemRectTransform.sizeDelta = new Vector2(itemSizeMin, itemSizeMin);
            itemRectTransform.anchoredPosition = new Vector2(pos.y * itemSpacingOffset + itemOffset.x, -pos.x * itemSpacingOffset - itemOffset.y);
            gridItems.Add(gridItem.GetComponent<GridItem>());
            gridItems.Last().Initialize(item);
            counter++;
        }

        // Adjust parent grid size
        rectTransform.sizeDelta = new Vector2(columns * itemSpacingOffset - itemsSpacing + horizontalPadding,
            rows * itemSpacingOffset - itemsSpacing + verticalPadding
        );

        return gridItems;
    }


    private void PreCalculate(Vector2 gridParentSize, int rows, int columns)
    {
        // Pre-calculate paddings and spacing
        float availableWidth = gridParentSize.x - horizontalPadding - (columns - 1) * itemsSpacing;
        float availableHeight = gridParentSize.y - verticalPadding - (rows - 1) * itemsSpacing;
        Vector2 itemSize = new Vector2(availableWidth / columns, availableHeight / rows);

        itemSizeMin = Mathf.Min(itemSize.x, itemSize.y);
        itemOffset = new Vector2(itemSizeMin / 2 + horizontalPadding / 2, itemSizeMin / 2 + verticalPadding / 2);
        itemSpacingOffset = itemSizeMin + itemsSpacing;
    }
}
