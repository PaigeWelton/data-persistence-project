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
    public TextMeshProUGUI nameText;
    public GameObject highScoreGroup;

    private HighScore highScore;

    public void Start()
    {
        ScoreManager.Instance.LoadNameAndScore();
        ScoreManager.Instance.scoresList.Sort();
        ScoreManager.Instance.scoresList.Reverse();

        if (ScoreManager.Instance.scoresList.Count != 0)
        {
            highScoreGroup.gameObject.SetActive(true);
            highScore = ScoreManager.Instance.scoresList[0];
            nameText.text = highScore.playerName + ": " + highScore.playerScore;
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
}
