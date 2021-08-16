using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TextMeshProUGUI nameText;
    public GameObject highScoreGroup;

    private string highScoreName;
    private int highScore;

    public void Start()
    {
        nameInput.onValueChanged.AddListener(delegate { NameChange(); }) ;

        ScoreManager.Instance.LoadNameAndScore();

        if (ScoreManager.Instance.highScoreName.Length != 0 && ScoreManager.Instance.highScore != 0)
        {
            highScoreGroup.gameObject.SetActive(true);
            highScoreName = ScoreManager.Instance.highScoreName;
            highScore = ScoreManager.Instance.highScore;
            nameText.text = highScoreName + ": " + highScore;
        }
        else
            highScoreGroup.gameObject.SetActive(false);

    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void NameChange()
    {
        ScoreManager.Instance.playerName = nameInput.text;
        ScoreManager.Instance.SaveNameAndScore();
    }
}
