using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoresDialog : MonoBehaviour
{

    public HighScoresChunk highScoreChunk;
    public GameObject chunkParent;

    private void Awake()
    {
        int listCountIndex = ScoreManager.Instance.scoresList.Count;
        for (int i = 0; i < listCountIndex; i++)
        {
            HighScoresChunk newChunk = Instantiate(highScoreChunk, chunkParent.transform);
            newChunk.Populate(ScoreManager.Instance.scoresList[i].playerName, ScoreManager.Instance.scoresList[i].playerScore);
            //newChunk.gameObject.transform.SetSiblingIndex(i);
        }
    }


    public void CloseDialog()
    {
        Destroy(gameObject);
    }
}
