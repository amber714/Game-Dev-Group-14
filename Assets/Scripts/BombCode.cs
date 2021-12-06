using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCode : MonoBehaviour
{
	[SerializeField] private GameObject particleEffect;
	[SerializeField] private GameObject explosion;
	private float timeToLive = 3.0F;
    private float createTime;
	
    // Start is called before the first frame update
    void Start()
    {
		Debug.Log("Bomb created!");
		createTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
		if (Time.time >= createTime + timeToLive){
			Debug.Log("Bomb Effect created!");
			GameObject effectInstance = (GameObject)Instantiate(particleEffect, transform.position, transform.rotation);
			Destroy(effectInstance, 2f);
			Debug.Log("Bomb created!");
			Instantiate(explosion, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
			Destroy(gameObject);
		}
    }
}