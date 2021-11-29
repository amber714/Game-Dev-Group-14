using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	
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
	public int game_playerHealth = 100;
	public int game_playerMaxHealth = 100;
	
	public int player_speed = 5;
	public float player_jump = 8.0F;
	public float player_gravity = 20.0F;
	
	public int game_gameState = 0; //States are [0,1,2,3] ... 0: startup, 1: running, 2: paused, 3: failed
	public int game_enemyTotal = 4;
	public int game_enemyRemaining = 4;
	
	private float game_time_countdown = 10;
	private float game_time_current;
	private	float game_time_delay = 0.5f;
	
	private AudioSource game_wavePassed;
	
	void Start() {
		
		game_wavePassed = this.GetComponent<AudioSource>();
		
		Time.timeScale = 1;
	
		pauseMenu.SetActive(false);
		failMenu.SetActive(false);
		
    }
	
	void Update() {
		
		coinsText.text = "" + game_coins;
		healthText.text = "" + game_playerHealth;
		
		if (game_gameState == 0) {
			
			if (game_time_countdown > 0) {
				waveText.text = "Time to start: " + (int)game_time_countdown;
				enemyText.text = "";
				game_time_countdown -= Time.deltaTime;
			} 
			
			if (game_time_countdown <= 0) {
				game_gameState = 1;
				spawnEnemies();
			}
			
		}
		
		if (game_gameState == 1) {
			enemyText.text = "Enemies: " + game_enemyRemaining;
			waveText.text = "Wave: " + game_wave;
		}
		
		if (game_enemyRemaining <= 0) {
			advanceWave();
		}
		
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Pause();
		}
		
		if (game_playerHealth <= 0) {
			GameOver();
		}
		
	}
	
	public void GameOver() {
		failMenu.SetActive(true);
		this.game_gameState = 3;
		Time.timeScale = 0;
    }
	
	public void LoadMenu() {
		SceneManager.LoadScene("Menu");
	}
	
	public void RestartLevel() {
		SceneManager.LoadScene("Game");
	}
	
	public void Pause() {
		pauseMenu.SetActive(true);
		this.game_gameState = 2;
		Time.timeScale = 0;
	}
	
	public void unPause() {
		pauseMenu.SetActive(false);
		this.game_gameState = 1;
		Time.timeScale = 1;
	}
	
	private void advanceWave() {
		game_wave += 1;
		game_enemyTotal = (2+(game_wave*2));
		game_enemyRemaining = game_enemyTotal;
		spawnEnemies();
    }
	
	private void spawnEnemies() {
		GameObject[] game_enemyArray = new GameObject[] { enemy1, enemy2, enemy3, enemy4 };
		for (int i = 0; i < this.game_enemyTotal; i++)
			Instantiate(game_enemyArray[Random.Range(0, game_enemyArray.Length)], new Vector3(Random.Range(-20, 20), 1, Random.Range(-20, 20)), Quaternion.identity);
	}
	
}
