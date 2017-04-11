using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public Enemy baseEnemyPrefab;
	public Enemy fastEnemyPrefab;
	public Enemy bigEnemyPrefab;

	// Use this for initialization
	void Start () {
		
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

	void Spawn(Enemy prefab){
		Enemy newEnemy = ObjectPooler.Spawn<Enemy> (prefab, this.transform.position);
		newEnemy.Init ();
	}
}
