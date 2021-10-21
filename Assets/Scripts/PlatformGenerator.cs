using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
	[SerializeField] private GameObject platform1;
	[SerializeField] private GameObject platform2;
	[SerializeField] private GameObject platform3;
	[SerializeField] private GameObject platform4;
	GameObject player;
	private Vector3 nextPlatformPos = Vector3.zero;
	private int platformChance;
	
    void Awake()
    {
		GameObject[] platformArray = new GameObject[] { platform1, platform2, platform3, platform4 };
		
        player = GameObject.FindGameObjectWithTag("Player");
		
		Instantiate(platformArray[Random.Range(0, platformArray.Length)], new Vector3(-25, 0, 25), Quaternion.identity);
		Instantiate(platformArray[Random.Range(0, platformArray.Length)], new Vector3(0, 0, 25), Quaternion.identity);
		Instantiate(platformArray[Random.Range(0, platformArray.Length)], new Vector3(25, 0, 25), Quaternion.identity);
		
		Instantiate(platformArray[Random.Range(0, platformArray.Length)], new Vector3(-25, 0, 0), Quaternion.identity);
		Instantiate(platformArray[Random.Range(0, platformArray.Length)], new Vector3(0, 0, 0), Quaternion.identity);
		Instantiate(platformArray[Random.Range(0, platformArray.Length)], new Vector3(25, 0, 0), Quaternion.identity);
		
		Instantiate(platformArray[Random.Range(0, platformArray.Length)], new Vector3(-25, 0, -25), Quaternion.identity);
		Instantiate(platformArray[Random.Range(0, platformArray.Length)], new Vector3(0, 0, -25), Quaternion.identity);
		Instantiate(platformArray[Random.Range(0, platformArray.Length)], new Vector3(25, 0, -25), Quaternion.identity);
    }
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
