using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour {
	// PUBLIC INSTANCE VARIABLES
	public Speed speed;
	public Boundary boundary;
    public GameController GM;
	public GameObject boom; //death explosion

    public bool resetcauseboom; //is the enemy resetting because it was destroyed or because it reached the end?

	// PRIVATE INSTANCE VARIABLES
	private float _CurrentSpeed;
	private float _CurrentDrift;

	private int bosslives = 10;
	
	public GameObject torpedoprefab;
	public float lastshot;
	public float shotdelay = 5;

	// Use this for initialization
	void Start () {
		this.GM = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		this._Reset ();
        
	}
	
	// Update is called once per frame
	void Update () {
		TryShooting ();
		Vector2 currentPosition = gameObject.GetComponent<Transform> ().position;
		currentPosition.y -= this._CurrentSpeed;
		gameObject.GetComponent<Transform> ().position = currentPosition;
		
		// Check bottom boundary
		if (currentPosition.y <= boundary.yMin) {
			this._Reset();
		}
	}

	private void TryShooting()
	{
		if(Time.time > lastshot + shotdelay){
			GM.PlayTorpedoSound();
			GameObject newtorp = Instantiate(torpedoprefab, this.transform.position, Quaternion.identity) as GameObject;
			newtorp.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation; 
			newtorp.transform.LookAt(GameObject.FindWithTag ("Player").transform.position);
			newtorp.GetComponent<BossTorpedo>().owner = this.gameObject;
			lastshot = Time.time;
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
		bosslives --;
		if(bosslives <=0)
		{
			Instantiate(boom, this.transform.position, Quaternion.identity);
			GM.gameover = true;
			Destroy (this.gameObject);

		}
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
