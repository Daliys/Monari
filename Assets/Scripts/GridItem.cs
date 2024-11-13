using System;
using DG.Tweening;
using Sounds;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is responsible for managing the grid item, handling user interactions, and animations
/// </summary>
[RequireComponent(typeof(Button))]
public class GridItem : MonoBehaviour
{
    [SerializeField] private Image frontImage;
    [SerializeField] private Image backImage;

    public int ImageId { get; private set; }
    public ItemData ItemData { get; private set; }
    public event Action<GridItem> OnFlip;

    private Button button;
    private Sprite cardFaceSprite;

    private bool isFlipped;
    private Tween animationTween;


    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }


    public void Initialize(ItemData itemData)
    {
        ItemData = itemData;
        cardFaceSprite = itemData.sprite;
        ImageId = itemData.id;
        isFlipped = false;
    }


    private void OnButtonClick()
    {
        if (isFlipped)
        {
            return;
        }

        FlipFront();
    }


    private void FlipFront()
    {
        isFlipped = true;
        frontImage.sprite = cardFaceSprite;
        OnFlip?.Invoke(this);

        SoundManager.Instance.PlayCardFlipSound();  
        animationTween = transform.DORotate(new Vector3(0, 90, 0), 0.2f).OnComplete(() =>
         {
             backImage.gameObject.SetActive(false);
             frontImage.gameObject.SetActive(true);
             transform.DORotate(new Vector3(0, 0, 0), 0.2f);

         });
    }


    public void FlipBack()
    {
        animationTween = DOVirtual.DelayedCall(0.5f, () =>
        {
            SoundManager.Instance.PlayCardFlipSound();
            transform.DORotate(new Vector3(0, 90, 0), 0.2f).OnComplete(() =>
                      {
                          frontImage.gameObject.SetActive(false);
                          backImage.gameObject.SetActive(true);
                          transform.DORotate(new Vector3(0, 0, 0), 0.2f);
                      });

            frontImage.sprite = null;
            isFlipped = false;
        });
    }


    public void DestroyItem(bool shouldPlayMusic = true, bool shouldWaitForFirstAnimation = true)
    {
        if (shouldWaitForFirstAnimation == false)
        {
            transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
            {
                // Here is not really good approach to destroy the game object, need to Create a pool of objects
                // but I don't have enough time to implement it, event though it will take a 20 minutes
                Destroy(gameObject);
            });
            return;
        }
        
        
        animationTween = DOVirtual.DelayedCall(0.5f, () =>
        {
            if (shouldPlayMusic)
            {
                SoundManager.Instance.PlayCardFlipSound();
            }

            transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
            {
                // Here is not really good approach to destroy the game object, need to Create a pool of objects
                // but I don't have enough time to implement it, event though it will take a 20 minutes
                Destroy(gameObject);
            });
        });
    }
    
    public void OnDestroy()
    {
        button.onClick.RemoveListener(OnButtonClick);
        animationTween.Kill();
    }

}
