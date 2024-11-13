using DG.Tweening;
using ScriptableObjects;
using Sounds;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameWinPanel : MonoBehaviour
    {
        [SerializeField] private CanvasGroup panelCanvasGroup;
        [SerializeField] private CanvasGroup gridCanvasGroup;
        [SerializeField] private TMP_Text congratsText;
        [SerializeField] private PanelAnimationSettings panelAnimationSettings;

        private void Start()
        {
            panelCanvasGroup.alpha = 0;
            panelCanvasGroup.transform.localScale = Vector3.zero;
        }


        private void Show()
        {
            if(gridCanvasGroup == null || panelCanvasGroup == null)
            {
                return;
            }
            
            
            SoundManager.Instance.PlayWinSound();
            Sequence sequence = DOTween.Sequence();
            // wait for the end of swapping animation it's 0.5sec
            sequence.AppendInterval(0.5f);
            sequence.Append(gridCanvasGroup.DOFade(0, panelAnimationSettings.fadeDuration));
            sequence.Append(panelCanvasGroup.DOFade(1, panelAnimationSettings.fadeDuration));
            sequence.Join(panelCanvasGroup.transform.DOScale(1, panelAnimationSettings.scaleDuration).SetEase(Ease.OutBack));
            
            sequence.Append(congratsText.transform.DOScale(panelAnimationSettings.pulseScale, panelAnimationSettings.textPulseDuration)
                .SetLoops(2, LoopType.Yoyo)
                .SetEase(Ease.InOutQuad));
        }

        private void Hide()
        {
            if(gridCanvasGroup == null || panelCanvasGroup == null)
            {
                return;
            }
            
            Sequence sequence = DOTween.Sequence();
            sequence.Append(panelCanvasGroup.DOFade(0, panelAnimationSettings.fadeDuration));
            sequence.Join(panelCanvasGroup.transform.DOScale(0, panelAnimationSettings.scaleDuration).SetEase(Ease.InBack));
            sequence.Append(gridCanvasGroup.DOFade(1, panelAnimationSettings.fadeDuration));
        }

        private void OnEnable()
        {
            GameLogic.OnGameWin += Show;
            GameUI.OnResetButtonClicked += Hide;
        }

        private void OnDisable()
        {
            GameLogic.OnGameWin -= Show;
            GameUI.OnResetButtonClicked -= Hide;
        }
    }
}