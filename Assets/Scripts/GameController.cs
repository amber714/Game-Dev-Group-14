using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	[SerializeField] private GameObject enemy1;
	[SerializeField] private GameObject enemy2;
	
	public int speed = 5;
	public int wave = 1;
	public int enemyCount = 4;
	public int remainingEnemies = 4;
	public float jumpIntensity = 5f;
	
    // Start is called before the first frame update
    void Awake() {
		Cursor.visible = false;
	}
	
	void Start()
    {
		spawnEnemies();
    }
	
	void Update()
	{
		if (remainingEnemies <= 0) {
			advanceWave();
		}
	}
	
	private void advanceWave()
    {
		Debug.Log("[DEBUG] advanceWave()");
		wave += 1;
		enemyCount = (2+(wave*2));
		remainingEnemies = enemyCount;
		spawnEnemies();
    }
	
	private void spawnEnemies()
	{
		GameObject[] enemyArray = new GameObject[] { enemy1, enemy2 };
		Debug.Log("enemyCount: " + this.enemyCount);
		for (int i = 0; i < this.enemyCount; i++)
			Instantiate(enemyArray[Random.Range(0, enemyArray.Length)], new Vector3(Random.Range(-20, 20), 1, Random.Range(-20, 20)), Quaternion.identity);
	}
	
}
