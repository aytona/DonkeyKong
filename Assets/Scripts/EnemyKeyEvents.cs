using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyKeyEvents : MonoBehaviour {

    List<GameObject> fireList = new List<GameObject>();

    public GameObject fire1;
    public GameObject fire2;

    void Start()
    {
        fireList.Add(fire1);
        fireList.Add(fire2);
    }

    void DestroyThis()
    {
        Destroy(this.gameObject);
    }

    void SpawnFire()
    {
        int fireIndex = UnityEngine.Random.Range(0, fireList.Count);
        Instantiate(fireList[fireIndex]);
    }
}
