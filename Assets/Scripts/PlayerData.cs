using UnityEngine;
using System.Collections;

public class PlayerData
{
    private static PlayerData instance = null;
    private int score = 0;
    private int highScore = 0;
    private int lives = 3;
    private int bonusScore = 5000;

    public static PlayerData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerData();
            }
            return instance;
        }
    }

    private PlayerData()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
    }

    public int Score
    {
        get
        {
            return this.score;
        }
        set
        {
            score = value;
            if (score > highScore)
            {
                this.highScore = score;
                PlayerPrefs.SetInt("HighScore", highScore);
            }
        }
    }

    public int HighScore
    {
        get
        {
            return this.highScore;
        }
    }

    public int Lives
    {
        get
        {
            return this.lives;
        }
        set
        {
            lives = value;
        }
    }

    public int BonusScore
    {
        get
        {
            return this.bonusScore;
        }
        set
        {
            bonusScore = value;
        }
    }
}
