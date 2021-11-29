using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	[SerializeField] private GameController gameController;
	[SerializeField] private GameObject playerCamera;
	
	public float camera_speed_up = 1.0F;
	public float camera_speed_down = 1.0F;
	
	private int camera_degrees = 10;
	private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;
	
	void Start()
	{
		
    }
	
    void Update () {
		
		CharacterController controller = GetComponent<CharacterController>();
		
		if(gameController.game_gameState == 1 || gameController.game_gameState == 0) {
			
			this.transform.Rotate(0, camera_degrees * Input.GetAxis("Mouse X"), 0);
			playerCamera.transform.RotateAround (this.transform.position, Vector3.up, 0);
			
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
		
    }
	
	
}