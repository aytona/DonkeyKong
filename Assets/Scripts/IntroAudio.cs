using UnityEngine;
using System.Collections;

public class IntroAudio : MonoBehaviour {

    private static IntroAudio instance = null;
    public static IntroAudio Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void OnLevelWasLoaded()
    {
        if (Application.loadedLevelName == "Level1")
        {
            Destroy(this.gameObject);
        }
    }
}
