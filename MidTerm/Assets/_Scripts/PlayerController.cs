using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	// PUBLIC INSTANCE VARIABLES +++++++++++++++++++++++++++++++++++++++
	public float speed;
	public Boundary boundary;

	public GameObject torpedoprefab;

	public Camera maincamera;

    public GameController GM;
	
	// PRIVATE INSTANCE VARIABLES
	private Vector2 _newPosition = new Vector2(0.0f, 0.0f);
	
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		this._CheckInput ();
	}

    public void ReduceLives() { GM.lives = GM.lives - 1; }

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
			GameObject newtorp = Instantiate(torpedoprefab, this.transform.position, Quaternion.identity) as GameObject;
			newtorp.GetComponent<Torpedo>().owner = this.gameObject;

		}

		this._BoundaryCheck ();

		gameObject.GetComponent<Transform>().position = this._newPosition;
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
