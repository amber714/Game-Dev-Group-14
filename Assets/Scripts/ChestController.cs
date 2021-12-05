using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestController : MonoBehaviour {
	
	[SerializeField] public GameObject chestPrice;
	[SerializeField] public GameObject chestHUD;
	
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
		
		if (Vector3.Distance(targetPlayer.transform.position, this.transform.position) < 5) {
			if(Input.GetKeyDown(KeyCode.E) && gameController.game_coins >= gameController.game_chestPrices) {
				openChest();
			} else if (Input.GetKeyDown(KeyCode.E) && gameController.game_coins < gameController.game_chestPrices){
				gameController.errorSound.Play();
			}
		}
		
	}
	
    void LateUpdate() {
        
		chestPrice.GetComponent<UnityEngine.TextMesh>().text = "" + gameController.game_chestPrices;
		
    }
	
	private void OnTriggerEnter(Collider other){
		
		if (other.gameObject.tag == "obstacle_water") {
			this.transform.position = new Vector3(Random.Range(-45, 45), 5, Random.Range(-45, 45));
        }
		
	}
	
	private void OnCollisionEnter(Collision collision) {
		
		if (collision.gameObject.tag == "Enemy") {
			Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), this.GetComponent<Collider>());
        }
		
    }
	
	private void openChest() {
		GameObject[] game_chestArray = new GameObject[] { gameController.powerup1, gameController.powerup2, gameController.powerup3, gameController.powerup4 };
		Instantiate(game_chestArray[Random.Range(0, game_chestArray.Length)], new Vector3(this.transform.position.x, this.transform.position.y + 2, this.transform.position.z), Quaternion.identity);
		gameController.game_coins -= gameController.game_chestPrices;
		gameController.game_chestPrices = (int)(gameController.game_chestPrices * 1.5);
		Destroy(gameObject);
	}
	
}
