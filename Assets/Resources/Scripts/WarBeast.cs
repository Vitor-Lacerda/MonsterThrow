using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarBeast : BigEnemy {

	public Enemy spawnedEnemyPrefab;
	public Transform spawnPosition;
	public Vector3 ejectForce;

	protected override void OnStartHit ()
	{
		base.OnStartHit ();
		SpawnEnemy ();
	}

	protected void SpawnEnemy(){
		Enemy spawnedEnemy = EnemySpawner.instance.Spawn (spawnedEnemyPrefab, spawnPosition.position, ejectForce);

	}


}
