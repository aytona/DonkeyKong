using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DKThrow : MonoBehaviour {

    List<GameObject> barrelList = new List<GameObject>();

    [SerializeField]private Transform specialBarrelSpawn = null;
    [SerializeField]private GameObject specialBarrel = null;
    [SerializeField]private Animator animator = null;

    public GameObject barrel1;
    public GameObject barrel2;
    public GameObject barrel3;
    public GameObject barrel4;
    public GameObject barrel5;

    private int numGen = 0;

    void Start()
    {
        barrelList.Add(barrel1);
        barrelList.Add(barrel2);
        barrelList.Add(barrel3);
        barrelList.Add(barrel4);
        barrelList.Add(barrel5);
        this.animator.SetTrigger("DKBlue");
    }

    // Random generator to determine the next move
    void nextMove()
    {
        numGen = Random.Range(1, 100);
        if (numGen >= 1 && numGen <= 40)
        {
            this.animator.SetTrigger("DKPound");
        }
        else if (numGen >= 41 && numGen <= 97)
        {
            this.animator.SetTrigger("DKThrow");
        }
        else if (numGen >= 98 && numGen <= 100)
        {
            this.animator.SetTrigger("DKBlue");
        }
        else
        {
            nextMove();
        }
    }

    // Instantiate barrel at the END of normal throw anim
    void normalThrow()
    {
        int barrelIndex = UnityEngine.Random.Range(0, barrelList.Count);
        Instantiate(barrelList[barrelIndex]);
        PlayerData.Instance.BonusScore -= 100;
    }

    // Instantiate barrel at the END of special throw anim
    void specialThrow()
    {
        GameObject specialBarrel = Object.Instantiate(this.specialBarrel) as GameObject;
        specialBarrel.transform.position = this.specialBarrelSpawn.transform.position;
        PlayerData.Instance.BonusScore -= 100;
    }
}
