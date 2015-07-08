using UnityEngine;
using System.Collections;

public class DKIntroKeyEvents : MonoBehaviour {

    // All the Straight Platforms
    public GameObject straightPlat1 = null;
    public GameObject straightPlat2 = null;
    public GameObject straightPlat3 = null;
    public GameObject straightPlat4 = null;
    public GameObject straightPlat5 = null;
    public GameObject straightPlat6 = null;

    // All the not straight platofrms
    public GameObject platform1 = null;
    public GameObject platform2 = null;
    public GameObject platform3 = null;
    public GameObject platform4 = null;
    public GameObject platform5 = null;
    public GameObject platform6 = null;

    void firstJump()
    {
        straightPlat1.SetActive(false);
        platform1.SetActive(true);
    }

    void secondJump()
    {
        straightPlat2.SetActive(false);
        platform2.SetActive(true);
    }

    void thirdJump()
    {
        straightPlat3.SetActive(false);
        platform3.SetActive(true);
    }

    void fourthJump()
    {
        straightPlat4.SetActive(false);
        platform4.SetActive(true);
    }

    void fifthJump()
    {
        straightPlat5.SetActive(false);
        platform5.SetActive(true);
    }

    void sixthJump()
    {
        straightPlat6.SetActive(false);
        platform6.SetActive(true);
    }

    void nextScene()
    {
        Application.LoadLevel("Level1");
    }
}
