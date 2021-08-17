using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class HighScore : IComparable<HighScore>
{
    public string playerName;
    public int playerScore;

    public HighScore(string newName, int newScore)
    {
        playerName = newName;
        playerScore = newScore;
    }

    public int CompareTo(HighScore other)
    {
        if (other == null)
            return 1;

        return playerScore - other.playerScore;
    }

}
