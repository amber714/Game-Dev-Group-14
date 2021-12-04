using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	[SerializeField] private GameController gameController;
	[SerializeField] private GameObject playerCamera;
	[SerializeField] private GameObject playerCameraFocus;
	
	[SerializeField] private GameObject powerup_prefab_turret;
	[SerializeField] private GameObject powerup_prefab_bombs;
	
	[SerializeField] private GameObject audioHandler1;
	[SerializeField] private GameObject audioHandler2;
	
	private int camera_degrees_x = 10;
	//private int camera_degrees_y = 0;
	private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;
	
	private AudioSource errorSound;
	
	void Start()
	{
		errorSound = audioHandler1.GetComponent<AudioSource>();
    }
	
    void Update () {
		
		CharacterController controller = GetComponent<CharacterController>();
		
		if(gameController.game_gameState == 1 || gameController.game_gameState == 0) {
			
			this.transform.Rotate(0, camera_degrees_x * Input.GetAxis("Mouse X"), 0);
			playerCamera.transform.RotateAround (this.transform.position, Vector3.up, 0);
			playerCamera.transform.LookAt(playerCameraFocus.transform);
			//playerCamera.transform.localPosition = new Vector3(playerCamera.transform.localPosition.x, 3 + camera_degrees_y, playerCamera.transform.localPosition.z);
			

			if (controller.isGrounded) {
				moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
				moveDirection = transform.TransformDirection(moveDirection);
				moveDirection *= gameController.player_speed;
				if (Input.GetButton("Jump")){
					//jumpSound.Play();
					moveDirection.y = gameController.player_jump;
				}
			}
			moveDirection.y -= gameController.player_gravity * Time.deltaTime;
			controller.Move(moveDirection * Time.deltaTime);
			
        }

		if(Input.GetKeyDown(KeyCode.Q)) {

			if (gameController.player_powerupEquip == 0) {
				errorSound.Play();
			}
			
			if (gameController.player_powerupEquip == 1) {
				
			}
			
			if (gameController.player_powerupEquip == 2) {
				for (int i = 0; i < 10; i++){
					Instantiate(powerup_prefab_bombs, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1, this.gameObject.transform.position.z), Quaternion.identity);
				}
			}
			
			if (gameController.player_powerupEquip == 3) {
				
			}
			
		}
		
    }
	
	private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag == "powerup_random") {
			if (gameController.player_powerupEquip == 0) {
				gameController.player_powerupEquip = 1;
				Destroy(other.gameObject);
			}
        }
		if (other.gameObject.tag == "powerup_tower") {
			if (gameController.player_powerupEquip == 0) {
				gameController.player_powerupEquip = 2;
				Destroy(other.gameObject);
			}
        }
		if (other.gameObject.tag == "powerup_speed") {
			if (gameController.player_powerupEquip == 0) {
				gameController.player_powerupEquip = 3;
				Destroy(other.gameObject);
			}
        }
		if (other.gameObject.tag == "powerup_bombs") {
			if (gameController.player_powerupEquip == 0) {
				gameController.player_powerupEquip = 4;
				Destroy(other.gameObject);
			}
        }
	}
	
}