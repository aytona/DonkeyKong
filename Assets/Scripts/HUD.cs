using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    public Text score;
    public Text highScore;

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

        this.score.text = PlayerData.Instance.Score.ToString();
        this.highScore.text = PlayerData.Instance.HighScore.ToString();
	}

    void OnLevelWasLoaded()
    {
        if (Application.loadedLevelName == "Menu" || Application.loadedLevelName == "Intro")
        {
            PlayerData.Instance.Score = 0;
            Destroy(this.gameObject);
        }
    }
}
