using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public partial class GameLogic : MonoBehaviour
{
    [SerializeField] private GameGridGenerator gridGenerator;
    [SerializeField] private CardImageSetSO cardImageSet;

    private List<GridItem> allItems;
    private GridItem firstFlippedItem;

    private Vector2Int gridSize = new Vector2Int(4, 4);

    private void Start()
    {
        if(gridSize.x * gridSize.y % 2 != 0)
        {
            Debug.LogError("Grid size must be even number");
            return;
        }

        allItems = gridGenerator.GenerateItems(gridSize.x, gridSize.y);
        InitializeItems();
    }

    private void InitializeItems()
    {
        int pairsCount = gridSize.x * gridSize.y / 2;
        int totalImagesCount = cardImageSet.cardImages.Count;

        if (pairsCount < totalImagesCount)
        {
            HashSet<ItemData> imageIds = new HashSet<ItemData>();
            while (imageIds.Count < pairsCount)
            {
                imageIds.Add(cardImageSet.GetRandomItem());
            }

            List<ItemData> imageIdsList = new List<ItemData>(imageIds);
            imageIdsList.AddRange(imageIdsList);

            foreach (GridItem item in allItems)
            {
                int randomIndex = Random.Range(0, imageIdsList.Count);
                item.Initialize(imageIdsList[randomIndex]);
                imageIdsList.RemoveAt(randomIndex);
                item.OnFlip += OnItemFlip;
            }
        }

    }

    private void OnItemFlip(GridItem item)
    {
        if (firstFlippedItem == null)
        {
            firstFlippedItem = item;
        }
        else
        {
            if (firstFlippedItem.ID == item.ID)
            {

                allItems.Remove(firstFlippedItem);
                allItems.Remove(item);

                Destroy(firstFlippedItem.gameObject);
                Destroy(item.gameObject);

                firstFlippedItem = null;

            }
            else
            {

                firstFlippedItem.FlipBack();
                item.FlipBack();
                 firstFlippedItem = null;

            }
        }
    }

}
