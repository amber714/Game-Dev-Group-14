using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class EnemyController : MonoBehaviour {
  
	[SerializeField] private float maxHealth;
	[SerializeField] private float currentHealth;
	[SerializeField] private int enemyValue;
	[SerializeField] private GameObject healthBar;
	
	public GameObject player;
	public GameController gameController;
    private Rigidbody myRigidbody;
    private Vector3 moveDirection;
	private NavMeshAgent agent;
	
	private AudioSource damagedSound;

  
    // Use this for initialization
    void Start () {
		damagedSound = GetComponent<AudioSource>();
		myRigidbody = GetComponent<Rigidbody>();
		agent = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag("Player");
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
      
    // Update is called once per frame
    void Update () {
		
		agent.SetDestination(player.transform.position); 
		
		healthBar.transform.localScale = new Vector3((currentHealth / maxHealth), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
		if (currentHealth <= 0) {
			Destroy(gameObject);
			gameController.remainingEnemies = gameController.remainingEnemies - 1;
		}
		
    }
	
	private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            Debug.Log("Projectile Collision!");
			damagedSound.Play();
			this.currentHealth -= 5;
			
			Destroy(other.gameObject);
        }
		
		if (other.gameObject.tag == "Explosion")
        {
            Debug.Log("Explosion Collision!");
			damagedSound.Play();
			this.currentHealth -= 15;
			
			//Destroy(other.gameObject);
        }
		
		if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player Collision!");
        }
    }

}