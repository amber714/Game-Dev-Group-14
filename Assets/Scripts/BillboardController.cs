using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardController : MonoBehaviour{
    
	[SerializeField] private GameObject proximityHUD;
	
	private GameObject billboard_targetPlayer;
	private Camera game_mainCamera;
	
    void Start() {
		billboard_targetPlayer = GameObject.FindGameObjectWithTag("Player");
        game_mainCamera = Camera.main;
		proximityHUD.SetActive(false);
    }

    void LateUpdate() {
		
        this.transform.rotation = game_mainCamera.transform.rotation;
		this.transform.rotation = Quaternion.Euler(0f, this.transform.rotation.eulerAngles.y, 0f);
		
		if (Vector3.Distance(billboard_targetPlayer.transform.position, this.transform.position) < 3) {
			proximityHUD.SetActive(true);
		} else {
			proximityHUD.SetActive(false);
		}
    }
}
