using UnityEngine;


/// <summary>
/// Represents the information needed to save an item, including its sprite ID and grid position.
/// </summary>
[System.Serializable]
public class SaveItemInfo
{
    /// <summary>
    /// The ID of the sprite associated with the item.
    /// </summary>
    public int spriteId;

    /// <summary>
    /// The position of the item on the grid.
    /// </summary>
    public Vector2Int gridPosition;


    public SaveItemInfo(int spriteId, Vector2Int gridPosition)
    {
        this.spriteId = spriteId;
        this.gridPosition = gridPosition;
    }
}
    
