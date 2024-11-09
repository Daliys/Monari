using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Manages the Main Menu UI, handling user interactions, input validations, and scene transitions.
/// </summary>
public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject LevelsPanel;
    [SerializeField] private TMP_InputField xInputField;
    [SerializeField] private TMP_InputField yInputField;
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private LevelDataSO levelDataSO;


    private void Start()
    {
        LevelsPanel.SetActive(false);
        xInputField.onValueChanged.AddListener((value) => OnInputFieldChanged(xInputField));
        yInputField.onValueChanged.AddListener((value) => OnInputFieldChanged(yInputField));
        errorText.text = "";
    }


    private void OnInputFieldChanged(TMP_InputField inputField)
    {
        if (int.TryParse(inputField.text, out int value))
        {
            if (value < 2 || value > 10)
            {
                inputField.text = "2";
                ShowErrorText("Value must be between 2 and 10");
            }
        }
        else
        {
            inputField.text = "2";
            ShowErrorText("Value must be a number");
        }
    }



    public void OnPlayNewLevelButtonClicked()
    {
        LevelsPanel.SetActive(true);
    }


    public void OnContinueButtonClicked()
    {

    }


    public void OnGenerateButtonClicked()
    {
        int x = int.Parse(xInputField.text);
        int y = int.Parse(yInputField.text);

        if (x * y % 2 != 0)
        {
            ShowErrorText("Grid size must be even number");
            return;
        }

        levelDataSO.gridSize = new Vector2Int(x, y);
        SceneManager.LoadScene(1);
    }

    private void ShowErrorText(string text)
    {
        errorText.text = text;
        DOVirtual.DelayedCall(3f, () => errorText.text = "");
    }
}
