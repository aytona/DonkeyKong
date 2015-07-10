using UnityEngine;
using System.Collections;

public class DKThrow : MonoBehaviour {

    [SerializeField]private Transform normalBarrelSpawn = null;
    [SerializeField]private Transform specialBarrelSpawn = null;
    [SerializeField]private GameObject specialBarrel = null;
    [SerializeField]private GameObject normalBarrel = null;
    [SerializeField]private Animator animator = null;

    private int numGen = 0;

    void Start()
    {
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
        GameObject normalBarrel = Object.Instantiate(this.normalBarrel) as GameObject;
        normalBarrel.transform.position = this.normalBarrelSpawn.transform.position;
    }

    // Instantiate barrel at the END of special throw anim
    void specialThrow()
    {
        GameObject specialBarrel = Object.Instantiate(this.specialBarrel) as GameObject;
        specialBarrel.transform.position = this.specialBarrelSpawn.transform.position;
    }
}
