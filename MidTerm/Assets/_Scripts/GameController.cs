using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 

public class GameController : MonoBehaviour {
	// PUBLIC INSTANCE VARIABLES
	public int enemyCount;
	public GameObject enemy;
    public GameObject player;
	public GameObject bigboss;
    public Camera maincamera;

    public AudioClip ding;
    public AudioClip ow;
    public AudioClip torpedo;

    public AudioSource source;

    public List<GameObject> enemies; //so we can get rid of them when it's time for the boss

    public int score;
    public int lives;

	public bool bosstime = false;
    public bool gameover = false;
	public GameObject boom; //death explosion
    
	public Text lifescore;

	//powerup prefabs
	public GameObject lifeup;
	public GameObject rateup;

	//temporary holders for newly instantiated objects
    private GameObject enemyhelper;
    private GameObject playerhelper;
	private GameObject boss;


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

	public void PlayTorpedoSound()
	{
		source.clip = torpedo;
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
		gamestate();
		UpdateGUI();
        TimeToDie();
	}

	private void gamestate()
	{
		if(bosstime && boss == null)
		{
			foreach(GameObject e in enemies){Destroy(e);}
			boss = Instantiate(bigboss);
		}
	}
	private void GameOver()
		{
			Application.LoadLevel("menu");
		}

	private void UpdateGUI()
	{
		if (lives > 0)
		{
		lifescore.text = "SCORE: " + score + " \n" + "LIFE: " + lives;
		}
		else{lifescore.text = "SCORE: " + score + " \n" + "GAME OVER";}
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
		player = playerhelper;
      playerhelper.GetComponent<PlayerController>().maincamera = maincamera;
      playerhelper.GetComponent<PlayerController>().GM = this;
    }

    private void TimeToDie(){

        if (lives <= 0) {
		{
			Instantiate(boom, player.transform.position, Quaternion.identity);
			Destroy (player.gameObject);
				InvokeRepeating ("GameOver", 5, 15);
        }
    }

	}}
