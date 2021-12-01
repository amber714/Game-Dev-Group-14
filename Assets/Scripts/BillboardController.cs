using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardController : MonoBehaviour{
    
	private Camera game_mainCamera;
	
    void Start() {
        game_mainCamera = Camera.main;
    }

    void LateUpdate() {
        this.transform.rotation = game_mainCamera.transform.rotation;
		this.transform.rotation = Quaternion.Euler(0f, this.transform.rotation.eulerAngles.y, 0f);
    }
}
