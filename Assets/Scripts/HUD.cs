using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    public Text score;
    public Text highScore;
    public Text bonusScore;
    public GameObject firstLife;
    public GameObject secondLife;

    private static HUD instance = null;

    void Awake()
    {
        if (instance == null)
        {
            GameObject.DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("Menu");
        }

        if (PlayerData.Instance.Lives == 2)
        {
            firstLife.SetActive(false);
        }
        else if (PlayerData.Instance.Lives == 1)
        {
            secondLife.SetActive(false);
        }
        else if (PlayerData.Instance.Lives == 3)
        {
            firstLife.SetActive(true);
            secondLife.SetActive(true);
        }
        if (PlayerData.Instance.BonusScore == 0)
        {
            PlayerData.Instance.Lives--;
        }

        this.score.text = PlayerData.Instance.Score.ToString();
        this.highScore.text = PlayerData.Instance.HighScore.ToString();
        this.bonusScore.text = PlayerData.Instance.BonusScore.ToString();
	}

    void OnLevelWasLoaded()
    {
        if (Application.loadedLevelName == "Menu" || Application.loadedLevelName == "Intro")
        {
            PlayerData.Instance.Score = 0;
            PlayerData.Instance.Lives = 3;
            PlayerData.Instance.BonusScore = 5000;
            Destroy(this.gameObject);
        }
    }
}
