using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestController : MonoBehaviour {
	
	[SerializeField] private GameController gameController;
	[SerializeField] private GameObject projectile1;
	[SerializeField] private GameObject projectile2;
	[SerializeField] private float maxHealth;
	[SerializeField] private float currentHealth;
	[SerializeField] private GameObject healthBar;

	public float camera_speed = 1.0F;
    public float jumpSpeed = 8.0F; 
    public float gravity = 20.0F;
	private float fireRate1 = 1F;
    private float nextFire1 = 0.0F;
	private float fireRate2 = 0.5F;
    private float nextFire2 = 0.0F;
    public float speed = 30;
    private Vector3 moveDirection = Vector3.zero;
 
    void Update() {
		
		healthBar.transform.localScale = new Vector3((currentHealth / maxHealth), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
		
        CharacterController controller = GetComponent<CharacterController>();
         // is the controller on the ground?
		if(Input.GetAxis("Mouse X") < 0)
			transform.Rotate((Vector3.up) * -camera_speed);
		if(Input.GetAxis("Mouse X") > 0)
			transform.Rotate((Vector3.up) * camera_speed);
		
		if(Input.GetKeyDown(KeyCode.Q) && Time.time > this.nextFire1) {
			this.nextFire1 = Time.time + this.fireRate1;
			Instantiate(projectile1, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1, gameObject.transform.position.z), Quaternion.identity);
		}
		
		if(Input.GetKeyDown(KeyCode.E) && Time.time > this.nextFire2) {
			this.nextFire2 = Time.time + this.fireRate2;
			Instantiate(projectile2, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1, gameObject.transform.position.z), Quaternion.identity);
		}

        if (controller.isGrounded) {
            //Feed moveDirection with input.
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            //Multiply it by speed.
            moveDirection *= gameController.speed;
            //Jumping
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
             
        }
		
		if (currentHealth <= 0)
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		
        //Applying gravity to the controller
        moveDirection.y -= gravity * Time.deltaTime;
        //Making the character move
        controller.Move(moveDirection * Time.deltaTime);
    }
	
	void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy Collision!");
			this.currentHealth -= 5;
        }
    }
	
	private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            Debug.Log("Trigger collision obstacle...");
        }
    }
}