using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the data to be saved and loaded for the game.
/// </summary>
[Serializable]
public partial class SaveData 
{
    public Vector2Int gridSize;
    public int pairsFoundCount;
    public int turnsCount;
    public int score;
    public int comboCount;

    /// <summary>
    /// Gets or sets the list of saved items.
    /// </summary>
    public List<SaveItemInfo> savedItems;


    public SaveData(Vector2Int gridSize, int pairsFoundCount, int turnsCount, int score, int comboCount, List<SaveItemInfo> savedItems)
    {
        this.gridSize = gridSize;
        this.pairsFoundCount = pairsFoundCount;
        this.turnsCount = turnsCount;
        this.score = score;
        this.comboCount = comboCount;
        this.savedItems = savedItems;
    }


    public SaveData()
    {
       
    }
}
