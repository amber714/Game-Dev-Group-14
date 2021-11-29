using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	[SerializeField] private GameObject enemy1;
	[SerializeField] private GameObject enemy2;
	[SerializeField] private GameObject enemy3;
	[SerializeField] private GameObject enemy4;
	
	[SerializeField] private GameObject powerup1;
	[SerializeField] private GameObject powerup2;
	[SerializeField] private GameObject powerup3;
	[SerializeField] private GameObject powerup4;
	
	[SerializeField] private Text waveText;
	[SerializeField] private Text enemyText;
	[SerializeField] private Text coinsText;
	[SerializeField] private Text healthText;
	
	[SerializeField] public GameObject pauseMenu;
	[SerializeField] public GameObject failMenu;
	
	[SerializeField] private GameObject player;
	
	public int game_coins;
	public int game_wave = 1;
	
	public int player_speed = 5;
	public float player_jump = 8.0F;
	
	public int game_enemyTotal = 4;
	public int game_enemyRemaining = 4;
	
	public float jumpIntensity = 5f;
	
	private float game_time_countdown = 10;
	private float game_time_current;
	private	float game_time_delay = 0.5f;
	
	private AudioSource game_wavePassed;
	
	void Start()
    {
		game_wavePassed = this.GetComponent<AudioSource>();
		
		waveText.text = "Wave: " + wave;
		
		spawningEnemies = 0; //Default 1
		Time.timeScale = 1;
		
		spawnEnemies();
    }
	
	void Update()
	{
		if (remainingEnemies <= 0) {
			advanceWave();
		}
	}
	
	private void advanceWave()
    {
		Debug.Log("[DEBUG] advanceWave()");
		wave += 1;
		enemyCount = (2+(wave*2));
		remainingEnemies = enemyCount;
		spawnEnemies();
    }
	
	private void spawnEnemies()
	{
		GameObject[] enemyArray = new GameObject[] { enemy1, enemy2 };
		Debug.Log("enemyCount: " + this.enemyCount);
		for (int i = 0; i < this.enemyCount; i++)
			Instantiate(enemyArray[Random.Range(0, enemyArray.Length)], new Vector3(Random.Range(-20, 20), 1, Random.Range(-20, 20)), Quaternion.identity);
	}
	
}
