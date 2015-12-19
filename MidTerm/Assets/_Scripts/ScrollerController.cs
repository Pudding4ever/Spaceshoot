using UnityEngine;
using System.Collections;

public class ScrollerController : MonoBehaviour {
	// PUBLIC INSTANCE VARIABLES
	public float speed;

	// Use this for initialization
	void Start () {
		this._Reset ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 currentPosition = gameObject.GetComponent<Transform> ().position;
		currentPosition.y -= speed;
		gameObject.GetComponent<Transform> ().position = currentPosition;

		// Check bottom boundary
		if (currentPosition.y <= -650) {
			this._Reset();
		}
	}

	private void _Reset() {
		Vector2 resetPosition = new Vector2 (0.0f, 650f);
		gameObject.GetComponent<Transform> ().position = resetPosition;
	}
}
