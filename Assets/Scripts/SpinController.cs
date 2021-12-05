using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinController : MonoBehaviour
{
    [SerializeField] private GameObject ring1;
	[SerializeField] private GameObject ring2;
	[SerializeField] private GameObject ring3;
	
	private GameObject targetPlayer;
	private GameController gameController;
	
	void Start() {
		targetPlayer = GameObject.FindGameObjectWithTag("Player");
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

    void Update() {
		
		if (Vector3.Distance(targetPlayer.transform.position, this.transform.position) < 4) {
			if (Input.GetKeyDown(KeyCode.E)) {
				CharacterController controller = targetPlayer.GetComponent<CharacterController>();
				controller.enabled = false;
				targetPlayer.transform.position = gameController.spawnPoint.transform.position;
				this.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);
				controller.enabled = true;
			}
		}
		
        ring1.transform.Rotate (75*Time.deltaTime,0,0);
		ring2.transform.Rotate (0,75*Time.deltaTime,0);
		ring3.transform.Rotate (75*Time.deltaTime,75*Time.deltaTime,0);
    }
}
