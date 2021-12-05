using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestController : MonoBehaviour {
	
	[SerializeField] public GameObject chestPrice;
	
	private GameController gameController;
	
    void Start() {
        
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		
    }
	
    void LateUpdate() {
        
		chestPrice.GetComponent<UnityEngine.TextMesh>().text = "" + gameController.game_chestPrices;
		
    }
	
	private void OnTriggerEnter(Collider other){
		
		if (other.gameObject.tag == "obstacle_water") {
			this.transform.position = new Vector3(Random.Range(-45, 45), 5, Random.Range(-45, 45));
        }
		
	}
	
	void OnCollisionEnter(Collision collision) {
		
		if (collision.gameObject.tag == "Enemy") {
			Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), this.GetComponent<Collider>());
        }
		
    }
	
}
