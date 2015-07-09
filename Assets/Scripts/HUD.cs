using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("Menu");
        }
	}
}
