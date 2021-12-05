using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookScript : MonoBehaviour {
	
	[SerializeField] private GameObject lookPoint;
	private GameObject targetPlayer;

    void Start() {
		targetPlayer = GameObject.FindGameObjectWithTag("Player");
	}

    void Update(){
        var lookPos = lookPoint.transform.position - this.transform.position;
		lookPos.x = targetPlayer.transform.position.x;
		lookPos.y = targetPlayer.transform.position.y;
		lookPos.z = targetPlayer.transform.position.z;
		var rotation = Quaternion.LookRotation(lookPos);
		transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime * 10);
    }
}
