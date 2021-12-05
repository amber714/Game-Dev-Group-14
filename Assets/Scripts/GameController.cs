using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	
	[SerializeField] public GameObject enemy1;
	[SerializeField] public GameObject enemy2;
	[SerializeField] public GameObject enemy3;
	[SerializeField] public GameObject enemy4;
	
	[SerializeField] public GameObject powerup1;
	[SerializeField] public GameObject powerup2;
	[SerializeField] public GameObject powerup3;
	[SerializeField] public GameObject powerup4;
	
	[SerializeField] public GameObject powerup1_icon;
	[SerializeField] public GameObject powerup2_icon;
	[SerializeField] public GameObject powerup3_icon;
	[SerializeField] public GameObject powerup4_icon;
	
	[SerializeField] public GameObject powerup_chest;
	
	[SerializeField] public Text waveText;
	[SerializeField] public Text enemyText;
	[SerializeField] public Text coinsText;
	[SerializeField] public Text healthText;
	[SerializeField] public Text speedText;
	[SerializeField] public Text jumpText;
	
	[SerializeField] public GameObject pauseMenu;
	[SerializeField] public GameObject failMenu;
	[SerializeField] public GameObject conversationHUD;
	[SerializeField] public Text conversationText;
	
	[SerializeField] public GameObject player;
	[SerializeField] public GameObject startPoint;
	
	[SerializeField] public AudioSource errorSound;
	[SerializeField] public AudioSource damagedSound;
	[SerializeField] public AudioSource pickupSound;
	[SerializeField] public AudioSource popSound;
	[SerializeField] public AudioSource jumpSound;
	[SerializeField] public AudioSource arenaMusic;
	[SerializeField] public AudioSource spawnMusic;
	
	public int game_coins;
	public int game_wave = 1;
	public int game_chestPrices = 25;
	
	public int player_currentHealth = 100;
	public int player_maxHealth = 100;
	public int player_speed = 5;
	public int player_powerupEquip = 0; //Powerups are [0,1,2] ... 0: none, 1: summon turret, 2: random, 3: speed boost, 4: bombs
	public float player_jump = 8.0F;
	public float player_gravity = 20.0F;
	
	public int game_spawnroom = 1;
	public int game_started = 0;
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
	
		conversationHUD.SetActive(false);
		pauseMenu.SetActive(false);
		failMenu.SetActive(false);
		
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = false;
		
		waveText.text = "";
		enemyText.text = "";
		
		for (int i = 0; i < 8; i++) {
			Instantiate(powerup_chest, new Vector3(Random.Range(-45, 45), 5, Random.Range(-45, 45)), Quaternion.Euler(new Vector3(0,Random.Range(0, 360),0)));
		}
		
		spawnMusic.Play();
		
    }
	
	void Update() {
		
		coinsText.text = "" + game_coins;
		healthText.text = "" + player_currentHealth;
		speedText.text = "" + player_speed;
		jumpText.text = "" + player_jump;
		
		if (game_gameState == 0 && game_spawnroom == 0) {
			
			if (game_time_countdown > 0) {
				waveText.text = "Time to start: " + (int)game_time_countdown;
				enemyText.text = "";
				game_time_countdown -= Time.deltaTime;
			} 
			
			if (game_time_countdown <= 0) {
				game_gameState = 1;
				game_started = 1;
				spawnEnemies();
			}
			
		}
		
		if (game_gameState == 1 && game_started == 1) {
			
			Cursor.lockState = CursorLockMode.Confined;
			Cursor.visible = false;
			
			enemyText.text = "Enemies: " + game_enemyRemaining;
			waveText.text = "Wave: " + game_wave;
		}
		
		if (game_enemyRemaining <= 0) {
			advanceWave();
		}
		
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Pause();
		}
		
		if (player_currentHealth <= 0) {
			GameOver();
		}
		
		if (player_powerupEquip == 0) {
			powerup1_icon.SetActive(false);
			powerup2_icon.SetActive(false);
			powerup3_icon.SetActive(false);
			powerup4_icon.SetActive(false);
		} else {
		
			if (player_powerupEquip == 1) {
				powerup1_icon.SetActive(true);
				powerup2_icon.SetActive(false);
				powerup3_icon.SetActive(false);
				powerup4_icon.SetActive(false);
			}
		
			if (player_powerupEquip == 2) {
				powerup1_icon.SetActive(false);
				powerup2_icon.SetActive(true);
				powerup3_icon.SetActive(false);
				powerup4_icon.SetActive(false);
			}
		
			if (player_powerupEquip == 3) {
				powerup1_icon.SetActive(false);
				powerup2_icon.SetActive(false);
				powerup3_icon.SetActive(true);
				powerup4_icon.SetActive(false);
			}
			
			if (player_powerupEquip == 4) {
				powerup1_icon.SetActive(false);
				powerup2_icon.SetActive(false);
				powerup3_icon.SetActive(false);
				powerup4_icon.SetActive(true);
			}
		
		}
		
	}
	
	public void GameOver() {
		failMenu.SetActive(true);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		this.game_gameState = 3;
		Time.timeScale = 0;
    }
	
	public void LoadMenu() {
		SceneManager.LoadScene("stage_title");
	}
	
	public void RestartLevel() {
		SceneManager.LoadScene("stage_green");
	}
	
	public void Pause() {
		pauseMenu.SetActive(true);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		this.game_gameState = 2;
		Time.timeScale = 0;
	}
	
	public void unPause() {
		pauseMenu.SetActive(false);
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = false;
		if (game_spawnroom == 0 && game_started == 1) {
			this.game_gameState = 1;
		}
		if (game_spawnroom == 1 && game_started == 0) {
			this.game_gameState = 0;
		}
		if (game_spawnroom == 0 && game_started == 0) {
			this.game_gameState = 0;
		}
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
		for (int i = 0; i < this.game_enemyTotal; i++) {
			Instantiate(game_enemyArray[Random.Range(0, game_enemyArray.Length)], new Vector3(Random.Range(-20, 20), 5, Random.Range(-20, 20)), Quaternion.identity);
		}
	}
	
}
