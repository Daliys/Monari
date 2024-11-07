
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardImageSet", menuName = "ScriptableObjec/CardImageSet")]
public class CardImageSetSO : ScriptableObject
{
    public List<Sprite> cardImages;

    public ItemData GetRandomItem()
    {
        int randomIndex = Random.Range(0, cardImages.Count);
        return new ItemData(cardImages[randomIndex], randomIndex);
    }
}
