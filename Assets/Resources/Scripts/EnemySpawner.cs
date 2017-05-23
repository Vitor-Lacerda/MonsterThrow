using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public static EnemySpawner instance;

	public Castle castle;

	public int enemyCount = 0;
	public int bigEnemyCount = 0;
	public float lowerHeight;
	public float upperHeight;

	public Transform enemyStartPoint;

	List<Enemy> enemies;

	void Awake(){
		if (instance == null) {
			instance = this;
		}
		enemies = new List<Enemy> ();
	}

	public void Reset(){
		foreach (Enemy e in enemies) {
			e.gameObject.SetActive (false);
		}
		enemyCount = 0;
	}





	public Enemy Spawn (Enemy prefab, Vector3 position, Vector3 force){
		Enemy newEnemy = ObjectPooler.Spawn<Enemy> (prefab, position);
		float startY = position.y;
		if (startY > upperHeight) {
			startY = Random.Range (lowerHeight, upperHeight);
		}
		if (!enemies.Contains (newEnemy)) {
			enemies.Add (newEnemy);
		}
		newEnemy.castle = castle;
		newEnemy.Init (startY, force);
		enemyCount++;
		if (newEnemy is BigEnemy) {
			bigEnemyCount++;
		}

		return newEnemy;
	}

	public Enemy Spawn(Enemy prefab, Vector3 position){
		return Spawn (prefab, position, Vector3.zero);
	}

	public Enemy Spawn(Enemy prefab){
		float newY = Random.Range (lowerHeight, upperHeight);
		return Spawn (prefab, new Vector3 (enemyStartPoint.position.x, newY, newY));
	}
}
