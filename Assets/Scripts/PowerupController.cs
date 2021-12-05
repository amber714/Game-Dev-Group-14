using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour {
	
	[SerializeField] public int powerupID;
	
	private GameObject targetPlayer;
	private GameController gameController;
	
	void Start() {
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		targetPlayer = GameObject.FindGameObjectWithTag("Player");
    }
	
	void Update() {
		
		if (Vector3.Distance(targetPlayer.transform.position, this.transform.position) < 4) {
			if (Input.GetKeyDown(KeyCode.E)) {
				gameController.pickupSound.Play();
				if (powerupID == 1) {
					gameController.player_powerupEquip = 1;
					Destroy(gameObject);
				}
				if (powerupID == 2) {
					gameController.player_powerupEquip = 2;
					Destroy(gameObject);	
				}
				if (powerupID == 3) {
					gameController.player_powerupEquip = 3;
					Destroy(gameObject);
				}
				if (powerupID == 4) {
					gameController.player_powerupEquip = 4;
					Destroy(gameObject);
				}
			}
		}
		
		transform.Rotate (0,50*Time.deltaTime,0);
		
	}
	
}