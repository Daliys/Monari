using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GridItem : MonoBehaviour
{
    [SerializeField] private Image image;

    public int ID { get ; private set; }
    public event Action<GridItem> OnFlip;

    private Button button;
 
    private Sprite cardFaceSprite;
    
    private bool isFlipped;



    private void Start() 
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);    
    }


    public void Initialize(ItemData itemData)
    {
        cardFaceSprite = itemData.sprite;
        ID = itemData.id;
        isFlipped = false;
    }


    private void OnButtonClick()
    {
        if(isFlipped)
        {
            return;
        }

        FlipFront();
    }

    public void FlipFront()
    {
        isFlipped = true;
        image.sprite = cardFaceSprite;
        OnFlip?.Invoke(this);
    }

    public void FlipBack()
    {
        DOVirtual.DelayedCall(0.5f, () =>
        {
            image.sprite = null;
            isFlipped = false;
        });
    }
}
