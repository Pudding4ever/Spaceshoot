using UnityEngine;
using System.Collections;

[System.Serializable]
public class Speed {
	public float minSpeed, maxSpeed;
}

[System.Serializable]
public class Boundary {
	public float xMin, xMax, yMin, yMax;
}

public class EnemyController : MonoBehaviour {
	// PUBLIC INSTANCE VARIABLES
	public Speed speed;
	public Boundary boundary;
    public GameController GM;
	public GameObject boom; //death explosion

    public bool resetcauseboom; //is the enemy resetting because it was destroyed or because it reached the end?

	// PRIVATE INSTANCE VARIABLES
	private float _CurrentSpeed;
	private float _CurrentDrift;

	// Use this for initialization
	void Start () {
		this._Reset ();
        
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 currentPosition = gameObject.GetComponent<Transform> ().position;
		currentPosition.y -= this._CurrentSpeed;
		gameObject.GetComponent<Transform> ().position = currentPosition;
		
		// Check bottom boundary
		if (currentPosition.y <= boundary.yMin) {
			this._Reset();
		}
	}

	//randomly rolls a powerup
	private void RandomRollPowerUp()
	{
		int index = Random.Range(1, 10);
		if (index == 1){Instantiate (GM.lifeup, this.transform.position, Quaternion.identity);}
		if (index == 2){Instantiate (GM.rateup, this.transform.position, Quaternion.identity);}
	}

	// resets the gameObject
	private void _Reset() {
		if (resetcauseboom) {RandomRollPowerUp();}
		this._CurrentSpeed = Random.Range (speed.minSpeed, speed.maxSpeed);
		Vector2 resetPosition = new Vector2 (Random.Range(boundary.xMin, boundary.xMax), boundary.yMax);
		gameObject.GetComponent<Transform> ().position = resetPosition;
		if (resetcauseboom) {GM.score = GM.score + 10; Debug.Log("Ding! Score up by 10. Current score: " + GM.score); }
        resetcauseboom = false;

	}

	public void TakeDamage(){
		ResetCauseBoom ();
	}

	public void ResetCauseBoom()
	{
		resetcauseboom = true;
		Reset();
	}

    public void Reset()
    {
		Instantiate (boom, this.transform.position, Quaternion.identity);
        _Reset();
    }

}
