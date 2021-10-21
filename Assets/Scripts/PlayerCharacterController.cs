using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
	[SerializeField] private GameController gameController;
	[SerializeField] private GameObject projectile1;
	[SerializeField] private bool isGrounded = true;
    
	// Start is called before the first frame update
    void Start()
    {
        if (!gameController)
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
		
		if (Input.GetKeyDown(KeyCode.W))
			transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, 1) * Time.deltaTime * gameController.speed);
		
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * gameController.jumpIntensity, ForceMode.Impulse);
        
		if(!isGrounded && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.down * gameController.jumpIntensity * 2, ForceMode.Impulse);
    }
	
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Platform")
			isGrounded = true;
	}
	
	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.tag == "Platform")
			isGrounded = false;
	}
	
}
