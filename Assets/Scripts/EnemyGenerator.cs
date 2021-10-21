using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
	[SerializeField] private GameController gameController;
	[SerializeField] private GameObject enemy1;
	[SerializeField] private GameObject enemy2;
	GameObject player;
	
    void Awake()
    {
		player = GameObject.Find("Player");
	}
	
    // Start is called before the first frame update
    void Start()
    {
		GameObject[] enemyArray = new GameObject[] { enemy1, enemy2 };
		Debug.Log("enemyCount: " + gameController.enemyCount);
		for (int i = 0; i < gameController.enemyCount; i++)
			Instantiate(enemyArray[Random.Range(0, enemyArray.Length)], new Vector3(Random.Range(-20, 20), 1, Random.Range(-20, 20)), Quaternion.identity);
    }
}
