using UnityEngine;
using System.Collections;

public class CanvasButtons : MonoBehaviour {
	public void StartGame() {
		Application.LoadLevel ("Level1");
	}

	public void MainMenu() {
		Application.LoadLevel ("Menu");
	}

	public void HowTo() {
		Application.LoadLevel ("HowTo");
	}

	public void Quit() {
		Application.Quit ();
	}
}
