using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	// PUBLIC INSTANCE VARIABLES
	public int enemyCount;
	public GameObject enemy;
    public GameObject player;
    public Camera maincamera;

    public AudioClip ding;
    public AudioClip ow;
    public AudioClip torpedo;

    public AudioSource source;

    public List<GameObject> enemies;

    public int score;
    public int lives;

    public bool gameover = false;
    
    private GameObject enemyhelper;
    private GameObject playerhelper;


    public void PlayOwSound()
    {
        source.clip = ow;
        source.Play();
    }

    public void PlayDingSound()
    {
        source.clip = ding;
        source.Play();
    }

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        enemies = new List<GameObject>();
        this._GeneratePlayer();
		this._GenerateEnemies ();
        lives = 5;
	}
	
	// Update is called once per frame
	void Update () {
        TimeToDie();
	}

	private void _GenerateEnemies() {
		for (int count=0; count < this.enemyCount; count++) {
			enemyhelper = Instantiate(enemy);
            enemyhelper.GetComponent<EnemyController>().GM = this;
            enemyhelper.GetComponent<EnemyController>().resetcauseboom = true;
            enemies.Add(enemyhelper);
            enemyhelper = null;
		}
	}
    private void _GeneratePlayer() 
    { playerhelper = Instantiate(player); 
      playerhelper.GetComponent<PlayerController>().maincamera = maincamera;
      playerhelper.GetComponent<PlayerController>().GM = this;
    }

    private void ResetGame()
    {
        foreach (GameObject t in enemies) { Destroy(t); }
        gameover = false;
        score = 0;
        lives = 5;
        _GenerateEnemies();
    }

    private void TimeToDie()
    {
        if (lives <= 0) {
            gameover = true;
            if (Input.GetKeyDown("r"))
            {
                ResetGame();
            }
        }
    }

}
