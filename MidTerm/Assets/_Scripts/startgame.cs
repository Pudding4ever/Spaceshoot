using UnityEngine;
using System.Collections;

public class startgame : MonoBehaviour {

public void StartGame()
	{
		Application.LoadLevel ("Main");
	}

	public void ExitGame()
	{
		Application.Quit ();
	}

}
