using DG.Tweening;
using Sounds;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [SerializeField] private Button continueButton;


    private void Start()
    {
        LevelsPanel.SetActive(false);
        xInputField.onEndEdit.AddListener((value) => OnEndEdit(value, xInputField));
        yInputField.onEndEdit.AddListener((value) => OnEndEdit(value, yInputField));
        errorText.text = "";
        
        SoundManager.Instance.PlayMenuMusic();
    }


    private void OnEndEdit(string input, TMP_InputField inputField)
    {
        if (int.TryParse(input, out int value))
        {
            if (value < 2 || value > 10)
            {
                ShowErrorText("Value must be between 2 and 10");
                inputField.text = "2";
            }
        }
        else
        {
            ShowErrorText("Value must be a number");
            inputField.text = "2";
        }
    }



    public void OnPlayNewLevelButtonClicked()
    {
        SoundManager.Instance.PlayButtonClickedSound();
        LevelsPanel.SetActive(true);
    }


    public void OnContinueButtonClicked()
    {
        SoundManager.Instance.PlayButtonClickedSound();
        SceneManager.LoadScene(1);
    }


    public void OnGenerateButtonClicked()
    {
        SoundManager.Instance.PlayButtonClickedSound();
        
        int x = int.Parse(xInputField.text);
        int y = int.Parse(yInputField.text);

        if (x * y % 2 != 0)
        {
            ShowErrorText("Grid size must be even number");
            return;
        }

        levelDataSO.gridSize = new Vector2Int(x, y);
        levelDataSO.saveData = null;
        SceneManager.LoadScene(1);
    }

    private void ShowErrorText(string text)
    {
        errorText.text = text;
        DOVirtual.DelayedCall(3f, () => errorText.text = "");
    }

    private void OnEnable()
    {
        if (SaveManager.HasKey(SaveDataKeys.GameProgress) == false)
        {
            continueButton.interactable = false;
            levelDataSO.saveData = null;
            return;
        }


        SaveData data = SaveManager.Load<SaveData>(SaveDataKeys.GameProgress);
        Debug.Log(data);

        continueButton.interactable = true;
        levelDataSO.saveData = data;

    }
}
