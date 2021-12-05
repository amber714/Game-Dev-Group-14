using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	
	[SerializeField] private GameController gameController;
	[SerializeField] private GameObject playerCamera;
	[SerializeField] private GameObject playerCameraFocus;
	
	[SerializeField] public GameObject ability1_icon;
	[SerializeField] public GameObject ability2_icon;
	[SerializeField] public GameObject[] ability3_icon = {};
	[SerializeField] public GameObject ability1_color;
	[SerializeField] public GameObject ability2_color;
	[SerializeField] public GameObject ability3_color;
	
	[SerializeField] public GameObject effect_slowdown;
	
	[SerializeField] private GameObject powerup_prefab_turret;
	[SerializeField] private GameObject powerup_prefab_bombs;
	
	[SerializeField] private GameObject audio_errorSound;
	[SerializeField] private GameObject audio_damagedSound;
	[SerializeField] private GameObject audio_weapon1Sound;
	[SerializeField] private GameObject audio_weapon2Sound;
	
	private int currentEffect = 0;
	private int control_enabled = 1;
	private int turret_placed = 0;
	private int camera_degrees = 10;
	private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;
	
	private float fireRate1 = 0.5F;
    private float nextFire1 = 0.0F;
	private float fireRate2 = 3.0F;
    private float nextFire2 = 0.0F;
	
	private float powerup_fireRate = 10.0F;
    private float powerup_nextFire = 0.0F;
	
	private AudioSource weapon1Sound;
	private AudioSource weapon2Sound;
	
	private GameObject player_turret;
	
	float mass = 3.0F; // defines the character mass
    Vector3 impact = Vector3.zero;
	
	void Start()
	{
		weapon1Sound = audio_weapon1Sound.GetComponent<AudioSource>();
		weapon2Sound = audio_weapon2Sound.GetComponent<AudioSource>();
    }
	
    void Update () {
		
		CharacterController controller = GetComponent<CharacterController>();
		
		if(gameController.game_gameState == 1 || gameController.game_gameState == 0) {
			
			this.transform.Rotate(0, camera_degrees * Input.GetAxis("Mouse X"), 0);
			playerCamera.transform.RotateAround (this.transform.position, Vector3.up, 0);
			playerCamera.transform.LookAt(playerCameraFocus.transform);
			

			if (controller.isGrounded && control_enabled == 1) {
				moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
				moveDirection = transform.TransformDirection(moveDirection);
				moveDirection *= gameController.player_speed;
				if (Input.GetButton("Jump")){
					gameController.jumpSound.Play();
					moveDirection.y = gameController.player_jump;
				}
			}
			
			if(Input.GetAxis("Mouse Y") < 0) {
				playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, playerCamera.transform.position.y + 0.05f, playerCamera.transform.position.z);
			}
			if(Input.GetAxis("Mouse Y") > 0) {
				playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, playerCamera.transform.position.y - 0.05f, playerCamera.transform.position.z);
			}
			
			// apply the impact force:
			if (impact.magnitude > 0.2F) { controller.Move(impact * Time.deltaTime); }
			// consumes the impact energy each cycle:
			impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
			
			moveDirection.y -= gameController.player_gravity * Time.deltaTime;
			controller.Move(moveDirection * Time.deltaTime);
			
        }
		
		if(Input.GetKey(KeyCode.Mouse0) && Time.time > this.nextFire1) {
			this.nextFire1 = Time.time + this.fireRate1;
			//weaponFlash.SetActive(true);
			//flashTime = Time.time;
			primaryFire();
		}
		
		if(Input.GetKey(KeyCode.Mouse1) && Time.time > this.nextFire2) {
			this.nextFire2 = Time.time + this.fireRate2;
			//weaponFlash.SetActive(true);
			//flashTime = Time.time;
			secondaryFire();
		}
		
		if (Time.time > this.nextFire1) { 
			ability1_icon.GetComponent<Image>().color = new Color32(255,255,255,255);
			ability1_color.GetComponent<Image>().color = new Color32(255,255,255,255);
		} 
		if (Time.time < this.nextFire1) { 
			ability1_icon.GetComponent<Image>().color = new Color32(255,255,255,150);
			ability1_color.GetComponent<Image>().color = new Color32(255,255,255,150);
		}
		
		if (Time.time > this.nextFire2) { 
			ability2_icon.GetComponent<Image>().color = new Color32(255,255,255,255);
			ability2_color.GetComponent<Image>().color = new Color32(255,255,255,255);
		} 
		if (Time.time < this.nextFire2) { 
			ability2_icon.GetComponent<Image>().color = new Color32(255,255,255,150);
			ability2_color.GetComponent<Image>().color = new Color32(255,255,255,150);
		}
		
		if (Time.time > this.powerup_nextFire) { 
			foreach (GameObject ability_icon in ability3_icon) {
				ability_icon.GetComponent<Image>().color = new Color32(255,255,255,255);
			}
			ability3_color.GetComponent<Image>().color = new Color32(255,255,255,255);
		} 
		if (Time.time < this.powerup_nextFire) { 
			foreach (GameObject ability_icon in ability3_icon) {
				ability_icon.GetComponent<Image>().color = new Color32(255,255,255,150);
			}
			ability3_color.GetComponent<Image>().color = new Color32(255,255,255,150);
		}

		if(Input.GetKeyDown(KeyCode.Q) && Time.time > this.powerup_nextFire) {

			if (gameController.player_powerupEquip == 0) {
				Debug.Log("[DEBUG] player_powerupEquip == 0");
				gameController.errorSound.Play();
			} else {
				this.powerup_nextFire = Time.time + this.powerup_fireRate;
				if (gameController.player_powerupEquip == 1) {
					Debug.Log("[DEBUG] player_powerupEquip == 1");
					controller.enabled = false;
					this.transform.position = new Vector3(Random.Range(-40, 40), 5, Random.Range(-40, 40));
					controller.enabled = true;
				}
				if (gameController.player_powerupEquip == 2) {
					Debug.Log("[DEBUG] player_powerupEquip == 2");
					if (turret_placed == 0) {
						player_turret = Instantiate(powerup_prefab_turret, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 3, this.gameObject.transform.position.z), Quaternion.identity);
						turret_placed = 1;
					} else if (turret_placed == 1) {
						player_turret.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 3, this.transform.position.z);
					}
				}
				if (gameController.player_powerupEquip == 3) {
					Debug.Log("[DEBUG] player_powerupEquip == 3");
					StartCoroutine("speedBoost");
				}
				if (gameController.player_powerupEquip == 4) {
					Debug.Log("[DEBUG] player_powerupEquip == 4");
					for (int i = 0; i < 15; i++){
						Instantiate(powerup_prefab_bombs, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1, this.gameObject.transform.position.z), Quaternion.identity);
					}
				}
			}
			
		}
		
    }
	
	public void AddImpact(Vector3 dir, float force){
		
        dir.Normalize();
        if (dir.y < 0) {
			dir.y = -dir.y;
		}
		// reflect down force on the ground
		impact += dir.normalized * force / mass;
		
    }
	
	private void OnTriggerEnter(Collider other){
		
		CharacterController controller = GetComponent<CharacterController>();
		
		if (other.gameObject.tag == "portal_start") {
			Debug.Log("[DEBUG] portal_start collision");
			controller.enabled = false;
			this.transform.position = gameController.startPoint.transform.position;
			this.transform.Rotate(0.0f, -122.0f, 0.0f, Space.Self);
			controller.enabled = true;
			gameController.game_spawnroom = 0;
			gameController.popSound.Play();
			gameController.arenaMusic.Play();
			gameController.spawnMusic.Stop();
        }
		
	}
	
	void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Enemy") {
            Debug.Log("Enemy Collision!");
			gameController.damagedSound.Play();
			gameController.player_currentHealth -= 10;
			var localVelocity = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
			//var localVelocity = new Vector3(0, 1, 0);
			AddImpact(localVelocity, 100);
			if (currentEffect == 0) {
				currentEffect = collision.gameObject.GetComponent<EnemyController>().enemy_effect;
				StartCoroutine("enemyEffect");
			}
        }
    }
	
	void primaryFire() {
		weapon1Sound.Play();
		if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hitInfo, 50)){
			Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * 50, Color.white, 0.5f, true); 
			Debug.Log(hitInfo.collider.name + " at " +  Time.time);
			
			if (hitInfo.collider.tag == "Enemy") {
				Debug.Log("Enemy shot!");
				hitInfo.collider.GetComponent<EnemyController>().enemy_currentHealth -= 5;
			}
		}
		
	}
	
	void secondaryFire() {
		StartCoroutine("secondaryBurst");
	}
	
	IEnumerator secondaryBurst() {
		for (int i = 0; i < 5; i++) {
			weapon1Sound.Play();
			if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hitInfo, 50)){
				Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * 50, Color.white, 0.5f, true); 
				//Debug.Log(hitInfo.collider.name + Time.time);
				
				if (hitInfo.collider.tag == "Enemy") {
					Debug.Log("Enemy shot!");
					hitInfo.collider.GetComponent<EnemyController>().enemy_currentHealth -= 5;
				}
			}
			yield return new WaitForSeconds(0.1f);
		}
	}
	
	IEnumerator speedBoost() {
		var stored_speed = gameController.player_speed;
		gameController.player_speed += 5;
		yield return new WaitForSeconds(3.0f);
		gameController.player_speed = stored_speed;
	}
	
	IEnumerator enemyEffect() {
		Debug.Log("Effect Triggered!");
		if (currentEffect == 1) {
			effect_slowdown.active = true;
			var stored_speed = gameController.player_speed;
			gameController.player_speed -= 5;
			if (gameController.player_speed <= 1) {
				gameController.player_speed = 1;
			}
			yield return new WaitForSeconds(1.0f);
			gameController.player_speed = stored_speed;
			effect_slowdown.active = false;
			currentEffect = 0;
		}
	}
	
}