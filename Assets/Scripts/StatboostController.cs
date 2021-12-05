using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatboostController : MonoBehaviour {

	[SerializeField] public int statboostID;
	
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
				if (statboostID == 1) { //Health
					gameController.player_currentHealth += 50;
					gameController.player_maxHealth += 10;
					if (gameController.player_currentHealth > gameController.player_maxHealth) {
						gameController.player_currentHealth = gameController.player_maxHealth;
					}
					Destroy(gameObject);
				}
				if (statboostID == 2) { //Speed
					gameController.player_speed += 1;
					Destroy(gameObject);	
				}
				if (statboostID == 3) { //Jump
					gameController.player_jump += 1.0f;
					Destroy(gameObject);
				}
			}
		}
		
        transform.Rotate (0,50*Time.deltaTime,0);
    }

}