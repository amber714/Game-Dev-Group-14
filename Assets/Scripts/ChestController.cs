using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour {
	
    void Start() {
        
    }
	
    void Update() {
        
    }
	
	private void OnTriggerEnter(Collider other){
		
		if (other.gameObject.tag == "obstacle_water") {
			this.transform.position = new Vector3(Random.Range(-45, 45), 5, Random.Range(-45, 45));
        }
		
	}
	
}
