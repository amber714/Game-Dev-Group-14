using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class EnemyController : MonoBehaviour {
	
	[SerializeField] public float enemy_currentHealth;
	[SerializeField] private float enemy_maxHealth;
	[SerializeField] private int enemy_value;
	[SerializeField] public int enemy_effect; //Effects are [0,1] ... 0: none, 1: slowdown
	[SerializeField] private GameObject enemy_healthBar;
	
	public GameObject enemy_targetPlayer;
	public GameController gameController;
	
	private Rigidbody myRigidbody;
    private Vector3 moveDirection;
	private NavMeshAgent agent;
	
    void Start() {
		
        myRigidbody = GetComponent<Rigidbody>();
		agent = GetComponent<NavMeshAgent>();
		enemy_targetPlayer = GameObject.FindGameObjectWithTag("Player");
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		
	}

    void Update() {
        
		agent.SetDestination(enemy_targetPlayer.transform.position); 
		
		enemy_healthBar.transform.localScale = new Vector3((enemy_currentHealth / enemy_maxHealth), 1, 1);
		if (enemy_currentHealth <= 0) {
			gameController.game_enemyRemaining = gameController.game_enemyRemaining - 1;
			gameController.game_coins += enemy_value;
			Destroy(gameObject);
		}
		
	}
	
	private void OnTriggerEnter(Collider other) {
		
        if (other.gameObject.tag == "Projectile") {
			
			Destroy(other.gameObject);
			this.enemy_currentHealth -= 5;
			
        }
		
		if (other.gameObject.tag == "Explosion") {
			
			Destroy(other.gameObject);
			this.enemy_currentHealth -= 30;
			
        }
		
	}
	
}
