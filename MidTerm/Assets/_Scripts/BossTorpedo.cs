using UnityEngine;
using System.Collections;

public class BossTorpedo : MonoBehaviour {

    float torpedodamage = 250;
	public GameObject owner;
	public float speed;

	

    void MoveTorpedo()
    {
		GetComponent<Rigidbody2D>().velocity=transform.forward * speed;
    }
	

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 3f);
	}
	
	// Update is called once per frame
	void Update () {
        MoveTorpedo();
	}
}
