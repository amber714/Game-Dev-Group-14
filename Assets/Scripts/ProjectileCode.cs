using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCode : MonoBehaviour
{
	public GameObject player;
	public GameObject enemy;
	private Vector3 moveDirection;
	private Rigidbody myRigidbody;
	
    // Start is called before the first frame update
    void Start()
    {
		myRigidbody = GetComponent<Rigidbody>();
		enemy = GameObject.FindGameObjectWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
		transform.rotation = Quaternion.LookRotation(Camera.main.transform.right);
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
    }

    // Update is called once per frame
    void Update() {
		
		moveDirection = Vector3.zero;
		
		if (Vector3.Distance(enemy.transform.position, this.transform.position) < 100) {
            Vector3 direction = enemy.transform.position - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
			if (direction.magnitude > .8)
                moveDirection = direction.normalized;
			myRigidbody.velocity = moveDirection * 10;
        }
    }
}
