using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyController : MonoBehaviour {
  
    [SerializeField] private float speed;
	[SerializeField] private float maxHealth;
	[SerializeField] private float currentHealth;
	[SerializeField] private int enemyValue;
	[SerializeField] private GameObject healthBar;
	
	public GameObject player;
	public GameController gameController;
    private Rigidbody myRigidbody;
    private Vector3 moveDirection;
  
    // Use this for initialization
    void Start () {
		myRigidbody = GetComponent<Rigidbody>();
		player = GameObject.FindGameObjectWithTag("Player");
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
      
    // Update is called once per frame
    void Update () {
	
        moveDirection = Vector3.zero;
		
		healthBar.transform.localScale = new Vector3((currentHealth / maxHealth), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
		if (currentHealth <= 0) {
			Destroy(gameObject);
			gameController.remainingEnemies = gameController.remainingEnemies - 1;
		}
	
        if (Vector3.Distance(player.transform.position, this.transform.position) < 50) {
            Vector3 direction = player.transform.position - this.transform.position;
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
		
            if (direction.magnitude > .8)
                moveDirection = direction.normalized;
			
			//Juggle between two, addforce works better, but need to fix acceration
			//myRigidbody.AddForce(moveDirection * speed);
			myRigidbody.velocity = moveDirection * speed;
        }
    }
	
	private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            Debug.Log("Projectile Collision!");
			this.currentHealth -= 5;
			
			Destroy(other.gameObject);
        }
		
		if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player Collision!");
        }
    }

}