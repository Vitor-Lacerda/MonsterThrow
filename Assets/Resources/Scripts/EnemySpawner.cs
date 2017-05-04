using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public Castle castle;

	public int enemyCount = 0;
	public float lowerHeight;
	public float upperHeight;

	public Transform enemyStartPoint;

	public void Spawn(Enemy prefab){
		float newY = Random.Range (lowerHeight, upperHeight);
		Enemy newEnemy = ObjectPooler.Spawn<Enemy> (prefab, new Vector3(enemyStartPoint.position.x, newY, newY) );
		newEnemy.castle = castle;
		newEnemy.Init ();
		enemyCount++;
	}
}
