using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public Castle castle;

	public int enemyCount = 0;

	public void Spawn(Enemy prefab){
		Enemy newEnemy = ObjectPooler.Spawn<Enemy> (prefab, this.transform.position);
		newEnemy.castle = castle;
		newEnemy.Init ();
		enemyCount++;
	}
}
