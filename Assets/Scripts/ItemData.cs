using System;
using UnityEngine;


public struct ItemData
{
    public Sprite sprite;
    public int id;
    public Vector2Int gridPosition;

    public ItemData(Sprite sprite, int id, Vector2Int gridPosition)
    {
        this.sprite = sprite;
        this.id = id;
        this.gridPosition = gridPosition;
    }

    public ItemData(Sprite sprite, int id)
    {
        this.sprite = sprite;
        this.id = id;
        this.gridPosition = Vector2Int.zero;
    }

    public void SetPosition(Vector2Int position)
    {
        gridPosition = position;
    }

}

