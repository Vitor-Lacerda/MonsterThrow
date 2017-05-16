using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public Castle castle;

	public int enemyCount = 0;
	public int bigEnemyCount = 0;
	public float lowerHeight;
	public float upperHeight;

	public Transform enemyStartPoint;

	List<Enemy> enemies;

	void Awake(){
		enemies = new List<Enemy> ();
	}

	public void Reset(){
		foreach (Enemy e in enemies) {
			e.gameObject.SetActive (false);
		}
		enemyCount = 0;
	}

	public void Spawn(Enemy prefab){
		float newY = Random.Range (lowerHeight, upperHeight);
		Enemy newEnemy = ObjectPooler.Spawn<Enemy> (prefab, new Vector3(enemyStartPoint.position.x, newY, newY) );
		if (!enemies.Contains (newEnemy)) {
			enemies.Add (newEnemy);
		}
		newEnemy.castle = castle;
		newEnemy.Init ();
		enemyCount++;
		if (newEnemy is BigEnemy) {
			bigEnemyCount++;
		}
	}
}
