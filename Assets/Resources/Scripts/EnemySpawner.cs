using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {


	public Castle castle;
	public float enemyInterval = 0.5f;
	public int maxEnemies = 15;

	public int enemyCount = 0;

	[Header("Enemy prefabs")]
	public Enemy baseEnemyPrefab;
	public Enemy fastEnemyPrefab;
	public Enemy bigEnemyPrefab;

	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnRoutine ());
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			Spawn (baseEnemyPrefab);
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			Spawn (fastEnemyPrefab);
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			Spawn (bigEnemyPrefab);
		}

	}

	IEnumerator SpawnRoutine(){
		while (true) {
			if (enemyCount < maxEnemies) {
				int r = Random.Range (0, 101);
				if (r <= 50) {
					Spawn (baseEnemyPrefab);
				} else if (r <= 99) {
					Spawn (fastEnemyPrefab);
				} else {
					Spawn (bigEnemyPrefab);
				}
			}

			yield return new WaitForSeconds (enemyInterval);
		}
	}

	void Spawn(Enemy prefab){
		Enemy newEnemy = ObjectPooler.Spawn<Enemy> (prefab, this.transform.position);
		newEnemy.castle = castle;
		newEnemy.Init ();
		enemyCount++;
	}
}
