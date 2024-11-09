using System;
using System.Collections.Generic;
using UnityEngine;

public partial class GameLogic : MonoBehaviour
{
    [SerializeField] private GameGridGenerator gridGenerator;
    [SerializeField] private CardImageSetSO cardImageSet;

    public static event Action<int> OnPairsFoundCountChanged;
    public static event Action<int> OnTurnsCountChanged;
    public static event Action OnGameWin;

    private int pairsFoundCount;
    private int totalPairsCount;
    private int turnsCount;

    private List<GridItem> allItems;
    private GridItem firstFlippedItem;

    private Vector2Int gridSize = new Vector2Int(4, 4);

    private void Start()
    {
        if (gridSize.x * gridSize.y % 2 != 0)
        {
            Debug.LogError("Grid size must be even number");
            return;
        }


        InitializeItems();
    }

    private void InitializeItems()
    {
        allItems = gridGenerator.GenerateItems(gridSize.x, gridSize.y);

        totalPairsCount = gridSize.x * gridSize.y / 2;
        int totalImagesCount = cardImageSet.cardImages.Count;

        if (totalPairsCount < totalImagesCount)
        {
            HashSet<ItemData> imageIds = new HashSet<ItemData>();
            while (imageIds.Count < totalPairsCount)
            {
                imageIds.Add(cardImageSet.GetRandomItem());
            }

            List<ItemData> imageIdsList = new List<ItemData>(imageIds);
            imageIdsList.AddRange(imageIdsList);

            foreach (GridItem item in allItems)
            {
                int randomIndex = UnityEngine.Random.Range(0, imageIdsList.Count);
                item.Initialize(imageIdsList[randomIndex]);
                imageIdsList.RemoveAt(randomIndex);
                item.OnFlip += OnItemFlip;
            }
        }

        turnsCount = 0;
        pairsFoundCount = 0;
        OnPairsFoundCountChanged?.Invoke(pairsFoundCount);
        OnTurnsCountChanged?.Invoke(turnsCount);
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
                pairsFoundCount++;
                turnsCount++;

                OnPairsFoundCountChanged?.Invoke(pairsFoundCount);
                OnTurnsCountChanged?.Invoke(turnsCount);

                if (pairsFoundCount == totalPairsCount)
                {
                    OnGameWin?.Invoke();
                }

            }
            else
            {

                firstFlippedItem.FlipBack();
                item.FlipBack();
                firstFlippedItem = null;
                turnsCount++;

                OnTurnsCountChanged?.Invoke(turnsCount);
            }
        }
    }

    private void Reset()
    {
        foreach (GridItem item in allItems)
        {
            Destroy(item.gameObject);
        }
        allItems.Clear();


        InitializeItems();
    }

    private void OnEnable()
    {
        GameUI.OnResetButtonClicked += Reset;
    }

    private void OnDisable()
    {
        GameUI.OnResetButtonClicked -= Reset;
    }

}
