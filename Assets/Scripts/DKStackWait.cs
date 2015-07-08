using UnityEngine;
using System.Collections;

public class DKStackWait : MonoBehaviour {

	void Start ()
    {
        StartCoroutine(delayScene());
	}

    private IEnumerator delayScene()
    {
        yield return new WaitForSeconds(3);
        Application.LoadLevel("Level1");
    }
}
