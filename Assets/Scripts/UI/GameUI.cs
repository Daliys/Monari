using System;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text matchesText;
    [SerializeField] private TMP_Text turnsText;

    public static event Action OnResetButtonClicked;


    public void UpdateMatches(int matches)
    {
        matchesText.text = matches.ToString();
    }

    public void UpdateTurns(int turns)
    {
        turnsText.text = turns.ToString();
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
        GameLogic.OnPairsFoundCountChanged += UpdateMatches;
        GameLogic.OnTurnsCountChanged += UpdateTurns;
    }

    private void OnDisable() 
    {
        GameLogic.OnPairsFoundCountChanged -= UpdateMatches;
        GameLogic.OnTurnsCountChanged -= UpdateTurns;
    }

}
