
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject that holds a set of card images.
/// </summary>
[CreateAssetMenu(fileName = "CardImageSet", menuName = "ScriptableObjects/CardImageSet")]
public class CardImageSetSO : ScriptableObject
{
    /// <summary>
    /// List of card images.
    /// </summary>
    public List<Sprite> cardImages;

    /// <summary>
    /// Gets a random item from the card images.
    /// </summary>
    /// <returns>An ItemData object containing a random card image and its index.</returns>
    public ItemData GetRandomItem()
    {
        int randomIndex = Random.Range(0, cardImages.Count);
        return new ItemData(cardImages[randomIndex], randomIndex);
    }
}
