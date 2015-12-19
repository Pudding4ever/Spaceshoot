using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("Collision detected!");
        if (col.tag == "Enemy") {
            Debug.Log("Enemy collison detected!");
            col.gameObject.GetComponent<EnemyController>().Reset();
            this.gameObject.GetComponent<PlayerController>().ReduceLives();
            this.gameObject.GetComponent<PlayerController>().GM.PlayOwSound();
        }

    }

	}

