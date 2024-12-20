using System;
using DG.Tweening;
using ScriptableObjects;
using Sounds;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    /// <summary>
    ///  This class is responsible for updating the UI elements of the game
    /// </summary>
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text matchesText;
        [SerializeField] private TMP_Text turnsText;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TextAnimationSettings textAnimationSettings;

        public static event Action OnResetButtonClicked;
        public static event Action OnButtonHomeClicked;

        private void Start()
        {
            SoundManager.Instance.PlayGameMusic();
        }

        private void UpdateMatches(int matches)
        {
            AnimateText(matchesText, matches.ToString());
        }

        private void UpdateTurns(int turns)
        {
            AnimateText(turnsText, turns.ToString());
        }
        
        private void UpdateScore(int score)
        {
            AnimateText(scoreText, score.ToString());
        }


        public void OnHomeButtonClicked()
        {
            SoundManager.Instance.PlayButtonClickedSound();
            OnButtonHomeClicked?.Invoke();
            DOVirtual.DelayedCall(0.55f, () => SceneManager.LoadScene(0));
        }

        public void OnButtonResetClicked()
        {
            SoundManager.Instance.PlayButtonClickedSound();
            OnResetButtonClicked?.Invoke();
        }
        
        private void AnimateText(TMP_Text text, string newValue)
        {
            if (text.text == newValue)
            {
                return;
            }
            
            if(text == null || textAnimationSettings == null)
            {
                return;
            }
            
            text.transform.DOScale(textAnimationSettings.regularTextScaleMultiplier, textAnimationSettings.regularTextAnimationDuration / 2)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    text.text = newValue;
                    text.transform.DOScale(1f, textAnimationSettings.regularTextAnimationDuration / 2).SetEase(Ease.InQuad);
                });
        }
        
    

        private void OnEnable() 
        {
            GameStatistic.OnPairsFoundCountChanged += UpdateMatches;
            GameStatistic.OnTurnsCountChanged += UpdateTurns;
            GameStatistic.OnScoreChanged += UpdateScore;
        }

        private void OnDisable() 
        {
            GameStatistic.OnPairsFoundCountChanged -= UpdateMatches;
            GameStatistic.OnTurnsCountChanged -= UpdateTurns;
            GameStatistic.OnScoreChanged -= UpdateScore;
        }

    }
}
