using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCode : MonoBehaviour
{
	public GameObject player;
	private float timeToLive = 0.5F;
    private float currentTime = 0.0F;
	
    // Start is called before the first frame update
    void Start()
    {
		currentTime = Time.time;
        player = GameObject.FindGameObjectWithTag("Player");
		transform.rotation = Quaternion.LookRotation(Camera.main.transform.right);
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
		if (Time.time >= currentTime + timeToLive)
			Destroy(gameObject);
		transform.rotation = Quaternion.LookRotation(Camera.main.transform.right);
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
    }
}
