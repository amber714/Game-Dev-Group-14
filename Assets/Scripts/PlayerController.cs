using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	[SerializeField] private GameController gameController;
	[SerializeField] private GameObject playerCamera;
	[SerializeField] private GameObject playerCameraFocus;
	
	[SerializeField] private GameObject powerup_prefab_turret;
	[SerializeField] private GameObject powerup_prefab_bombs;
	
	[SerializeField] private GameObject hitMarker;
	
	[SerializeField] private GameObject audioHandler1;
	[SerializeField] private GameObject audio_weaponPrimary;
	[SerializeField] private GameObject audio_weaponSecondary;
	
	private int control_enabled = 1;
	private int camera_degrees = 10;
	private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;
	private float fireRate1 = 0.5F;
    private float nextFire1 = 0.0F;
	
	private AudioSource errorSound;
	private AudioSource weapon1Sound;
	private AudioSource weapon2Sound;
	
	void Start()
	{
		errorSound = audioHandler1.GetComponent<AudioSource>();
		weapon1Sound = audio_weaponPrimary.GetComponent<AudioSource>();
		weapon2Sound = audio_weaponSecondary.GetComponent<AudioSource>();
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
					//jumpSound.Play();
					moveDirection.y = gameController.player_jump;
				}
			}
			
			if(Input.GetAxis("Mouse Y") < 0)
				playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, playerCamera.transform.position.y + 0.05f, playerCamera.transform.position.z);
			if(Input.GetAxis("Mouse Y") > 0)
				playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, playerCamera.transform.position.y - 0.05f, playerCamera.transform.position.z);
			
			moveDirection.y -= gameController.player_gravity * Time.deltaTime;
			controller.Move(moveDirection * Time.deltaTime);
			
        }
		
		if(Input.GetKeyDown(KeyCode.Mouse0) && Time.time > this.nextFire1) {
			this.nextFire1 = Time.time + this.fireRate1;
			weapon1Sound.Play();
			//weaponFlash.SetActive(true);
			//flashTime = Time.time;
			primaryFire();
		}

		if(Input.GetKeyDown(KeyCode.Q)) {

			if (gameController.player_powerupEquip == 0) {
				Debug.Log("[DEBUG] player_powerupEquip == 0");
				errorSound.Play();
			}
			
			if (gameController.player_powerupEquip == 1) {
				Debug.Log("[DEBUG] player_powerupEquip == 1");
			}
			
			if (gameController.player_powerupEquip == 2) {
				Debug.Log("[DEBUG] player_powerupEquip == 2");
				Instantiate(powerup_prefab_turret, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 3, this.gameObject.transform.position.z), Quaternion.identity);
			}
			
			if (gameController.player_powerupEquip == 3) {
				Debug.Log("[DEBUG] player_powerupEquip == 3");
			}
			
			if (gameController.player_powerupEquip == 4) {
				Debug.Log("[DEBUG] player_powerupEquip == 4");
				for (int i = 0; i < 10; i++){
					Instantiate(powerup_prefab_bombs, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1, this.gameObject.transform.position.z), Quaternion.identity);
				}
			}
			
		}
		
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
        }
		
	}
	
	void primaryFire()
	{
		if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hitInfo, 50)){
			Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * 50, Color.white, 0.5f, true); 
			Debug.Log(hitInfo.collider.name + Time.time);
			if (hitInfo.collider.tag == "Enemy") {
				Debug.Log("Enemy shot!");
				hitInfo.collider.GetComponent<EnemyController>().enemy_currentHealth -= 5;
			}
		}

	}
	
}