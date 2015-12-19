using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("Collision detected!");
		if(col.CompareTag("Life")){Debug.Log ("LIFE"); this.gameObject.GetComponent<PlayerController>().LifeUp();Destroy(col.gameObject);}
		if(col.CompareTag("Rate")){this.gameObject.GetComponent<PlayerController>().RateUp(); Destroy(col.gameObject);}
        if (col.tag == "Enemy") 
		{
            Debug.Log("Enemy collison detected!");
			col.gameObject.GetComponent<EnemyController>().Reset();
            this.gameObject.GetComponent<PlayerController>().ReduceLives();
            this.gameObject.GetComponent<PlayerController>().GM.PlayOwSound();
        }
		if (col.tag == "BossTorpedo")
		{
			Destroy(col.gameObject);
			this.gameObject.GetComponent<PlayerController>().ReduceLives();
			this.gameObject.GetComponent<PlayerController>().GM.PlayOwSound();
		}




    }

	}

