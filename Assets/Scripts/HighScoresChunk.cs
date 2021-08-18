using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoresChunk : MonoBehaviour
{

    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI scoreText;

    public void Populate(string playerName, int score)
    {
        playerNameText.text = playerName;
        scoreText.text = score.ToString();
    }
}
