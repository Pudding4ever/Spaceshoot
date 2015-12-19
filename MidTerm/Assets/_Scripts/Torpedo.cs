using UnityEngine;
using System.Collections;

public class Torpedo : MonoBehaviour {

    float torpedodamage = 250;
	public GameObject owner;

	

    void MoveTorpedo()
    {
        Vector3 movement = new Vector3();
        movement = transform.up * (0.1f);
        this.transform.position = this.transform.position + movement;
    }

    void OnTriggerEnter(Collider obj)
    {
        Debug.Log("collision detected!");
        if (obj.gameObject.CompareTag("enemy")){
            Debug.Log("enemy!");
            EnemyController enemy = obj.gameObject.GetComponent("EnemyController") as EnemyController;
            enemy.TakeDamage();
            Destroy(this.gameObject);
    }}

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 3f);
	}
	
	// Update is called once per frame
	void Update () {
        MoveTorpedo();
	}
}
