using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {
    
	[SerializeField] private GameObject npc_portrait;
	[SerializeField] private string[] npc_dialogue = {};
	
	private GameObject targetPlayer;
	private GameController gameController;
	
    void Start() {
		
		targetPlayer = GameObject.FindGameObjectWithTag("Player");
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		
	}

    void Update() {
        
		if (Vector3.Distance(targetPlayer.transform.position, this.transform.position) < 10) {
			var lookPos = targetPlayer.transform.position - this.transform.position;
			lookPos.y = 0;
			var rotation = Quaternion.LookRotation(lookPos);
			transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime * 10);
			
			if(Input.GetKeyDown(KeyCode.E)) {
				interactNPC();
			}
		}
		
	}
	
	void interactNPC() {
		npc_portrait.SetActive(true);
		gameController.conversationHUD.SetActive(true);
		StartCoroutine("AutoType");
	}
	
	IEnumerator AutoType () {
		foreach(string dialogue in npc_dialogue){
			foreach(char character in dialogue.ToCharArray()){
				gameController.conversationText.text += character;
				yield return new WaitForSeconds(0.05f);
			}
			yield return new WaitForSeconds(1f);
			gameController.conversationText.text = "";
		}
		npc_portrait.SetActive(false);
		gameController.conversationHUD.SetActive(false);
	}
	
}
