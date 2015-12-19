using UnityEngine;
using System.Collections;

public class Torpedo : MonoBehaviour {

    float torpedodamage = 250;
	public GameObject owner;
	public float speed;

	

    void MoveTorpedo()
    {
		GetComponent<Rigidbody2D>().velocity=transform.forward * speed;
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        Debug.Log("torpedo collision detected!");
        if (obj.gameObject.CompareTag("Enemy")){
            Debug.Log("enemy!");
            EnemyController enemy = obj.gameObject.GetComponent("EnemyController") as EnemyController;
            enemy.TakeDamage();
			owner.GetComponent<PlayerController>().killedenemies ++;
            Destroy(this.gameObject);
    }
		if (obj.gameObject.CompareTag ("Boss"))
		    {
			BossController enemy = obj.gameObject.GetComponent("BossController") as BossController;
			enemy.TakeDamage();
			Destroy(this.gameObject);
			}
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
