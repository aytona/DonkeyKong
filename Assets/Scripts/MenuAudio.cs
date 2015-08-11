using UnityEngine;
using System.Collections;

public class MenuAudio : MonoBehaviour {

    private static MenuAudio instance = null;
    public static MenuAudio Instance
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
        if (Application.loadedLevelName == "Intro")
        {
            Destroy(this.gameObject);
        }
    }
}
