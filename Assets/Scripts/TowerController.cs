using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class TowerController : MonoBehaviour {
	
	[SerializeField] private GameObject hitMarkerWand;
	[SerializeField] private int range = 50;
	
	private GameObject enemy;
	private float time_current;
	private float attack_delay = 1f;
	private GameObject currentTarget;
	
    // Update is called once per frame
    void Update() {
		enemy = GameObject.FindGameObjectWithTag("Enemy");
		if(enemy != null) {
			if (Vector3.Distance(enemy.transform.position, this.transform.position) < range) {
				this.transform.LookAt(enemy.transform);
				currentTarget = enemy;
				if (Time.time >= time_current) {
					//attack enemy
					time_current = Time.time + attack_delay;
					Instantiate(hitMarkerWand, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
				}
			}
		}
	}
	
}