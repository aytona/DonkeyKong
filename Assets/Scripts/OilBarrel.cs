using UnityEngine;
using System.Collections;

public class OilBarrel : MonoBehaviour {
    [SerializeField]private GameObject fire = null;
    [SerializeField]private Transform spawnPoint = null;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Barrel")
        {
            Destroy(other.gameObject);
            GameObject fireSprite = Object.Instantiate(this.fire) as GameObject;
            fireSprite.transform.position = this.spawnPoint.transform.position;
        }
    }
}
