using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileCode : MonoBehaviour
{
	public GameObject player;
	private Vector3 moveDirection;
	private Rigidbody myRigidbody;
	
    // Start is called before the first frame update
    void Start()
    {
		myRigidbody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
		transform.rotation = Quaternion.LookRotation(Camera.main.transform.right);
		transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
    }

    // Update is called once per frame
    void Update() {
		
		moveDirection = Vector3.zero;
		
		if (Vector3.Distance(player.transform.position, this.transform.position) < 50) {
            Vector3 direction = player.transform.position - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
			if (direction.magnitude > .8)
                moveDirection = direction.normalized;
			myRigidbody.velocity = moveDirection * 10;
        }
    }
	
	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Explosion") {
			Destroy(gameObject);
        }
		if (other.gameObject.tag == "hitMarker") {
			Destroy(gameObject);
			Destroy(other.gameObject);
        }
	}
}
