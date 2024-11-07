using UnityEngine;


public record ItemData
{
    public Sprite sprite;
    public int id;

    public ItemData(Sprite sprite, int id)
    {
        this.sprite = sprite;
        this.id = id;
    }
}

