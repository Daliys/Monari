using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class GameLogic : MonoBehaviour
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
    private Vector2Int gridSize;

    // need to track if the game has changed to save the data
    private bool hasChanged = false;


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

        InitializeItems();
    }


    private void InitializeItems()
    {
        gameStatistic = new GameStatistic(scoreInformation);
        hasChanged = true;

        List<ItemData> itemDatas = GenerateItemDatas(gridSize);
        allItems = gridGenerator.GenerateItemsWith(gridSize.x, gridSize.y, itemDatas);

        // Subscribe to the flip event of the grid items & set the sprite of the item
        foreach (GridItem item in allItems)
        {
            item.OnFlip += OnItemFlip;
        }

        gameStatistic.ResetAll();
    }


    /// <summary>
    ///  Generate a list of item datas for the grid based on the grid size
    /// </summary>
    private List<ItemData> GenerateItemDatas(Vector2Int gridSize)
    {
        List<ItemData> itemDatas = new();
        totalPairsCount = gridSize.x * gridSize.y / 2;
        int totalImagesCount = cardImageSet.cardImages.Count;

        // If the number of card images is less than the total number of pairs, we just take the random images,
        // because it is anyway going to be repeated
        if (totalImagesCount < totalPairsCount)
        {
            while (itemDatas.Count < totalPairsCount)
            {
                itemDatas.Add(cardImageSet.GetRandomItem());
            }
            // Duplicate the item datas to create pairs of items for the grid
            itemDatas.AddRange(itemDatas);
        }
        else  // get only unique images
        {
            HashSet<ItemData> itemHashSet = new();
            // Generate a set of unique item datas, here we are using a hashset to ensure uniqueness 
            // (if pairs count is less than total images)
            while (itemHashSet.Count < totalPairsCount)
            {
                ItemData itemData = cardImageSet.GetRandomItem();
                itemHashSet.Add(itemData);
            }

            // Duplicate the item datas to create pairs of items for the grid
            itemDatas.AddRange(itemHashSet);
            itemDatas.AddRange(itemDatas);
        }

        // Shuffle the item datas
        for (int i = 0; i < itemDatas.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, itemDatas.Count);
            (itemDatas[i], itemDatas[randomIndex]) = (itemDatas[randomIndex], itemDatas[i]);
        }

        // Assign grid positions to the item datas
        for (int i = 0; i < itemDatas.Count; i++)
        {
            ItemData itemData = itemDatas[i];
            itemData.gridPosition = new Vector2Int(i % gridSize.x, i / gridSize.x);
            itemDatas[i] = itemData;
        }

        return itemDatas;
    }


    private void OnItemFlip(GridItem item)
    {
        hasChanged = true;
        if (firstFlippedItem == null)
        {
            firstFlippedItem = item;
        }
        else
        {
            if (firstFlippedItem.ImageId == item.ImageId)
            {
                allItems.Remove(firstFlippedItem);
                allItems.Remove(item);

                firstFlippedItem.DestroyItem();
                item.DestroyItem();

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


    private void SaveData()
    {
        if (hasChanged == false)
        {
            return;
        }

        // If there are no items, delete the saved data
        if (allItems.Count == 0)
        {
            SaveManager.DeleteKey(SaveDataKeys.GameProgress);
            return;
        }

        int pairsFoundCount = gameStatistic.PairsFoundCount;
        int turnsCount = gameStatistic.TurnsCount;
        int score = gameStatistic.Score;
        int comboCount = gameStatistic.ComboCount;

        List<SaveItemInfo> savedItems = new();
        foreach (GridItem item in allItems)
        {
            savedItems.Add(new SaveItemInfo(item.ImageId, item.ItemData.gridPosition));
        }

        SaveData saveData = new SaveData(gridSize, pairsFoundCount, turnsCount, score, comboCount, savedItems);
        SaveManager.Save(SaveDataKeys.GameProgress, saveData);
    }


    private void LoadSavedData()
    {
        SaveData saveData = levelData.saveData;
        gridSize = saveData.gridSize;
        gameStatistic = new GameStatistic(scoreInformation, saveData.pairsFoundCount, saveData.turnsCount, saveData.score, saveData.comboCount);
        totalPairsCount = gridSize.x * gridSize.y / 2;
        // Convert the saved items to item datas

        List<ItemData> itemDatas = new();
        foreach (SaveItemInfo saveItem in saveData.savedItems)
        {
            Sprite sprite = cardImageSet.cardImages[saveItem.spriteId];
            ItemData itemData = new(sprite, saveItem.spriteId, saveItem.gridPosition);
            itemDatas.Add(itemData);
        }

        allItems = gridGenerator.GenerateItemsWith(gridSize.x, gridSize.y, itemDatas);

        foreach (GridItem item in allItems)
        {
            item.OnFlip += OnItemFlip;
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
        GameUI.OnButtonHomeClicked += SaveData;
    }


    private void OnDisable()
    {
        GameUI.OnResetButtonClicked -= Reset;
        GameUI.OnButtonHomeClicked -= SaveData;
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveData();
        }
    }

    private void OnApplicationQuit() => SaveData();

}
