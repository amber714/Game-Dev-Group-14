using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatboostController : MonoBehaviour {
    
	[SerializeField] private GameObject proximityHUD;
	[SerializeField] private GameObject statIcon;
	[SerializeField] public int statboostID;
	
	private GameObject targetPlayer;
	private GameController gameController;
	
	void Start() {
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		targetPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        transform.Rotate (0,50*Time.deltaTime,0);
    }

}