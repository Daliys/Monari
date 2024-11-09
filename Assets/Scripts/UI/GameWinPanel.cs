using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameWinPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text congratsText;
    
    public void Show()
    {
        panel.SetActive(true);
       

    }

    public void Hide()
    {
        panel.SetActive(false);
    }

    private void OnEnable() {
        GameLogic.OnGameWin += Show;
        GameUI.OnResetButtonClicked += Hide;
    }
    private void OnDisable() {
        GameLogic.OnGameWin -= Show;
        GameUI.OnResetButtonClicked -= Hide;
    }
}
