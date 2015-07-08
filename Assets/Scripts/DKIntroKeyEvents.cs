using UnityEngine;
using System.Collections;

public class DKIntroKeyEvents : MonoBehaviour {

    // All the straight platforms
    public GameObject straightPlat1 = null;
    public GameObject straightPlat2 = null;
    public GameObject straightPlat3 = null;
    public GameObject straightPlat4 = null;
    public GameObject straightPlat5 = null;
    public GameObject straightPlat6 = null;

    // All the not straight platoforms
    public GameObject platform1 = null;
    public GameObject platform2 = null;
    public GameObject platform3 = null;
    public GameObject platform4 = null;
    public GameObject platform5 = null;
    public GameObject platform6 = null;

    // All set ladders
    public GameObject setLadder0 = null;
    public GameObject setLadder1 = null;
    public GameObject setLadder2 = null;
    public GameObject setLadder3 = null;
    public GameObject setLadder4 = null;
    public GameObject setLadder5 = null;
    public GameObject setLadder6 = null;
    public GameObject setLadder7 = null;
    public GameObject setLadder8 = null;
    public GameObject setLadder9 = null;
    public GameObject setLadder10 = null;
    public GameObject setLadder11 = null;
    public GameObject setLadder12 = null;
    public GameObject setLadder13 = null;
    public GameObject setLadder14 = null;
    public GameObject setLadder15 = null;
    public GameObject setLadder16 = null;
    public GameObject setLadder17 = null;
    public GameObject setLadder18 = null;
    public GameObject setLadder19 = null;
    public GameObject setLadder20 = null;
    public GameObject setLadder21 = null;
    public GameObject setLadder22 = null;

    void ladderZero()
    {
        setLadder0.SetActive(false);
    }

    void ladderOne()
    {
        setLadder1.SetActive(false);
    }

    void ladderTwo()
    {
        setLadder2.SetActive(false);
    }

    void ladderThree()
    {
        setLadder3.SetActive(false);
    }

    void ladderFour()
    {
        setLadder4.SetActive(false);
    }

    void ladderFive()
    {
        setLadder5.SetActive(false);
    }

    void ladderSix()
    {
        setLadder6.SetActive(false);
    }

    void ladderSeven()
    {
        setLadder7.SetActive(false);
    }

    void ladderEight()
    {
        setLadder8.SetActive(false);
    }

    void ladderNine()
    {
        setLadder9.SetActive(false);
    }

    void ladderTen()
    {
        setLadder10.SetActive(false);
    }

    void ladderEleven()
    {
        setLadder11.SetActive(false);
    }

    void ladderTwelve()
    {
        setLadder12.SetActive(false);
    }

    void ladderThirteen()
    {
        setLadder13.SetActive(false);
    }

    void ladderFourteen()
    {
        setLadder14.SetActive(false);
    }

    void ladderFifteen()
    {
        setLadder15.SetActive(false);
    }

    void ladderRest()
    {
        setLadder16.SetActive(false);
        setLadder17.SetActive(false);
        setLadder18.SetActive(false);
        setLadder19.SetActive(false);
        setLadder20.SetActive(false);
        setLadder21.SetActive(false);
        setLadder22.SetActive(false);
    }

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
        Application.LoadLevel("DKStack");
    }

}
