using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	// PUBLIC INSTANCE VARIABLES +++++++++++++++++++++++++++++++++++++++
	public float speed;
	public Boundary boundary;
	public bool speedboost = false;

	//torpedo statistics
	public GameObject torpedoprefab;
	public float lastshot;
	public float shotdelay = 1;


	public Camera maincamera;

    public GameController GM;

	public int killedenemies; //game controller changes game state to make boss appear when 50 enemies are killed.
	
	// PRIVATE INSTANCE VARIABLES
	private Vector2 _newPosition = new Vector2(0.0f, 0.0f);
	
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		this._CheckInput ();
		Afterburner();
		BossTime();
	}
	
    public void ReduceLives() { GM.lives = GM.lives - 1; }

	void BossTime()
	{
		if(killedenemies >= 25)
		{
			GM.bosstime = true;
		}
	}

	void _CheckInput() {
		this._newPosition = gameObject.GetComponent<Transform> ().position; // current position
	
		if (Input.GetAxis ("Horizontal") > 0) {
			this._newPosition.x += this.speed; // add move value to current position
		}
	
		
		if (Input.GetAxis ("Horizontal") < 0) {
			this._newPosition.x -= this.speed; // subtract move value to current position
		}

		if (Input.GetAxis ("Vertical") > 0) {
			this._newPosition.y += this.speed; // add move value to current position
		}
		
		
		if (Input.GetAxis ("Vertical") < 0) {
			this._newPosition.y -= this.speed; // subtract move value to current position
		}

		if(Input.GetButton ("Fire1")){
			if(Time.time > lastshot + shotdelay){
				GM.PlayTorpedoSound();
			GameObject newtorp = Instantiate(torpedoprefab, this.transform.position, Quaternion.identity) as GameObject;
			newtorp.transform.eulerAngles = new Vector3(270, 0, 0);
			newtorp.GetComponent<Torpedo>().owner = this.gameObject;
			lastshot = Time.time;
			}


		}
		if(Input.GetButtonDown ("Fire2")){
			speedboost = !speedboost;
		}
		this._BoundaryCheck ();

		gameObject.GetComponent<Transform>().position = this._newPosition;
	}

	public void Afterburner()
	{
		if (speedboost)
		{
			speed = 12;
		}
		else{speed = 6;}
	}

	public void LifeUp()
	{
		GM.lives ++;
	}

	public void RateUp()
	{
		if (this.shotdelay > 0.4f)
		{
		this.shotdelay -= 0.1f;
		}
	}

	private void _BoundaryCheck() {
		if (this._newPosition.x < this.boundary.xMin) {
			this._newPosition.x = this.boundary.xMin;
		}

		if (this._newPosition.x > this.boundary.xMax) {
			this._newPosition.x = this.boundary.xMax;
		}

		if (this._newPosition.y < this.boundary.yMin) {
			this._newPosition.y = this.boundary.yMin;
		}
		
		if (this._newPosition.y > this.boundary.yMax) {
			this._newPosition.y = this.boundary.yMax;
		}
	}
}
