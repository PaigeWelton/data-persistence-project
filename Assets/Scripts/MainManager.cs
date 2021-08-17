using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;

    public GameObject newHighScoreGroup;
    public TMP_InputField nameInput;
    public Button nameEnterButton;

    private string playerName;
    private bool isTypingName;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        GameOverText.gameObject.SetActive(false);
        newHighScoreGroup.gameObject.SetActive(false);
        isTypingName = false;
        ScoreManager.Instance.LoadNameAndScore();

        if (ScoreManager.Instance.scoresList.Count != 0)
        {
            BestScoreText.gameObject.SetActive(true);
            string highScoreName = ScoreManager.Instance.scoresList[0].playerName;
            int highScore = ScoreManager.Instance.scoresList[0].playerScore;
            BestScoreText.text = "Best Score: " + highScoreName + ": " + highScore;
        }
        else
            BestScoreText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!m_Started && !isTypingName)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver && !isTypingName)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        UpdateHighScore();
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    public void UpdateHighScore()
    {
        if (ScoreManager.Instance.scoresList.Count == 0)
        {
            newHighScoreGroup.gameObject.SetActive(true);
            isTypingName = true;
            nameInput.onValueChanged.AddListener(delegate { NameButtonValidation(); });
        }
        else
        {
            ScoreManager.Instance.scoresList.Sort();
            ScoreManager.Instance.scoresList.Reverse();
            int scoresListLastIndex = ScoreManager.Instance.scoresList.Count - 1;
            int lowestScore = ScoreManager.Instance.scoresList[scoresListLastIndex].playerScore;
            if (m_Points > lowestScore)
            {
                newHighScoreGroup.gameObject.SetActive(true);
                isTypingName = true;
                nameInput.onValueChanged.AddListener(delegate { NameButtonValidation(); });
            }
        }
    }

    public void NameButtonValidation()
    {
        if (nameInput.text.Length == 0)
        {
            nameEnterButton.interactable = false;
        }
        else
            nameEnterButton.interactable = true;
    }

    public void NameChange()
    {
        playerName = nameInput.text;
        isTypingName = false;
        SaveNewScore();
        newHighScoreGroup.SetActive(false);
    }

    public void SaveNewScore()
    {
        ScoreManager.Instance.scoresList.Add(new HighScore(playerName, m_Points));
        ScoreManager.Instance.scoresList.Sort();
        ScoreManager.Instance.scoresList.Reverse();

        if (ScoreManager.Instance.scoresList.Count > 10)
        {
            ScoreManager.Instance.scoresList.RemoveAt(10);
        }

        foreach (HighScore score in ScoreManager.Instance.scoresList)
        {
            Debug.Log(score.playerName + ": " + score.playerScore);
        }

        UpdateHighestScoreUI();
        ScoreManager.Instance.SaveNameAndScore();
    }

    public void UpdateHighestScoreUI()
    {
        BestScoreText.gameObject.SetActive(true);
        string highScoreName = ScoreManager.Instance.scoresList[0].playerName;
        int highScore = ScoreManager.Instance.scoresList[0].playerScore;
        BestScoreText.text = "Best Score: " + highScoreName + ": " + highScore;
    }
}
