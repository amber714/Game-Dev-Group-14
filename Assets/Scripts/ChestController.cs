using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestController : MonoBehaviour {
	
	[SerializeField] public GameObject chestPrice;
	[SerializeField] public GameObject chestHUD;
	[SerializeField] private int chestID; // ID: 1 = statusboosts // ID: 2 = abilityitems
	
	private GameController gameController;
	private GameObject targetPlayer;
	
    void Start() {
        
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		targetPlayer = GameObject.FindGameObjectWithTag("Player");
		chestHUD.SetActive(false);
		
    }
	
	void Update() {
		
		if (Vector3.Distance(targetPlayer.transform.position, this.transform.position) < 20) {
			chestHUD.SetActive(true);
		} else {
			chestHUD.SetActive(false);
		}
		
		if (Vector3.Distance(targetPlayer.transform.position, this.transform.position) < 4) {
			if(Input.GetKeyDown(KeyCode.E) && gameController.game_coins >= gameController.game_chestPrices) {
				openChest();
			} else if (Input.GetKeyDown(KeyCode.E) && gameController.game_coins < gameController.game_chestPrices){
				gameController.errorSound.Play();
			}
		}
		
	}
	
    void LateUpdate() {
        if (chestID == 1) {
			chestPrice.GetComponent<UnityEngine.TextMesh>().text = "" + gameController.game_chestPrices;
		}
		if (chestID == 2) {
			chestPrice.GetComponent<UnityEngine.TextMesh>().text = "" + gameController.game_chestPrices2;
		}
    }
	
	private void OnTriggerEnter(Collider other){
		
		if (other.gameObject.tag == "obstacle_water") {
			this.transform.position = new Vector3(Random.Range(-40, 40), 5, Random.Range(-40, 40));
        }
		
	}
	
	private void OnCollisionEnter(Collision collision) {
		
		if (collision.gameObject.tag == "Enemy") {
			Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), this.GetComponent<Collider>());
        }
		
    }
	
	private void openChest() {
		if (chestID == 1) {
			GameObject[] game_chestArray = new GameObject[] { gameController.statboost1, gameController.statboost2, gameController.statboost3 };
			Instantiate(game_chestArray[Random.Range(0, game_chestArray.Length)], new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z), Quaternion.identity);
			gameController.game_coins -= gameController.game_chestPrices;
			gameController.game_chestPrices = (int)(gameController.game_chestPrices * 1.2);
			Instantiate(gameController.statboost_chest, new Vector3(Random.Range(-40, 40), 5, Random.Range(-40, 40)), Quaternion.Euler(new Vector3(0,Random.Range(0, 360),0)));
			Destroy(gameObject);
		}
		if (chestID == 2) {
			GameObject[] game_chestArray = new GameObject[] { gameController.powerup1, gameController.powerup2, gameController.powerup3, gameController.powerup4 };
			Instantiate(game_chestArray[Random.Range(0, game_chestArray.Length)], new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z), Quaternion.identity);
			gameController.game_coins -= gameController.game_chestPrices2;
			gameController.game_chestPrices2 = (int)(gameController.game_chestPrices2 * 1.5);
			Instantiate(gameController.statboost_chest, new Vector3(Random.Range(-40, 40), 5, Random.Range(-40, 40)), Quaternion.Euler(new Vector3(0,Random.Range(0, 360),0)));
			Destroy(gameObject);
		}
	}
	
}
