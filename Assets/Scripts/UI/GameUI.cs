using System;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text matchesText;
    [SerializeField] private TMP_Text turnsText;
    [SerializeField] private TMP_Text scoreText;

    public static event Action OnResetButtonClicked;


    public void UpdateMatches(int matches)
    {
        matchesText.text = matches.ToString();
    }

    public void UpdateTurns(int turns)
    {
        turnsText.text = turns.ToString();
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }


    public void OnHomeButtonClicked()
    {
       
    }

    public void OnButtonResetClicked()
    {
        OnResetButtonClicked?.Invoke();
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
