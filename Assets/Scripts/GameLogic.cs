using System;
using System.Collections.Generic;
using UnityEngine;

public partial class GameLogic : MonoBehaviour
{
    [SerializeField] private GameGridGenerator gridGenerator;
    [SerializeField] private CardImageSetSO cardImageSet;
    [SerializeField] private ScoreInformationSO scoreInformation;
    [SerializeField] private LevelDataSO levelData;

    public static event Action OnGameWin;

    private GameStatistic gameStatistic;

    private int totalPairsCount;

    private List<GridItem> allItems;
    private GridItem firstFlippedItem;

    private Vector2Int gridSize = new Vector2Int(4, 4);

    private void Start()
    {
        // Load saved data if available
        if (levelData.saveData != null)
        {
            LoadSavedData();
            return;
        }

        // otherwise, initialize the game with the level data
        gridSize = levelData.gridSize;

        if (gridSize.x * gridSize.y % 2 != 0)
        {
            Debug.LogError("Grid size must be even number");
            return;
        }

        gameStatistic = new GameStatistic(scoreInformation);
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

        gameStatistic.ResetAll();
    }

    private void LoadSavedData()
    {
        // for loading saved data
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
                gameStatistic.OnCompleteSwap(true);


                if (gameStatistic.PairsFoundCount == totalPairsCount)
                {
                    OnGameWin?.Invoke();
                }

            }
            else
            {

                firstFlippedItem.FlipBack();
                item.FlipBack();
                firstFlippedItem = null;
                
                gameStatistic.OnCompleteSwap(false);
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
