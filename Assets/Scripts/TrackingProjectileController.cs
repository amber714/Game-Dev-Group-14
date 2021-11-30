using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingProjectileController : MonoBehaviour
{
	public GameObject player;
	private GameObject enemy;
	private Vector3 moveDirection;
	private Rigidbody myRigidbody;
	
    // Start is called before the first frame update
    void Start()
    {
		myRigidbody = GetComponent<Rigidbody>();
		enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    // Update is called once per frame
    void Update() {
		
		if(!enemy) {
			Destroy(gameObject);
		} else {
			moveDirection = Vector3.zero;
			if (Vector3.Distance(enemy.transform.position, this.transform.position) < 50) {
				Vector3 direction = enemy.transform.position - this.transform.position;
				if (direction.magnitude > .8)
					moveDirection = direction.normalized;
				myRigidbody.velocity = moveDirection * 10;
			}
		}
    }
}