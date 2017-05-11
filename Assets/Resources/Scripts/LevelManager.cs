using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	[System.Serializable]
	public struct LevelEnemies
	{
		public Enemy enemyPrefab;
		public int quantity;
	}

	[System.Serializable]
	public struct LevelInfo
	{
		public List<LevelEnemies> enemies;
		public float enemySpawnInterval;
	}

	public EnemySpawner enemySpawner;
	public List<LevelInfo> levels;



	int currentLevel = -1;

	void Start () {
	}

	void Update () {
		
	}

	public void NextLevel(){
		if (currentLevel + 1 < levels.Count) {
			StartLevel (currentLevel + 1);
		}
	}

	void StartLevel(int index){
		Debug.Log ("Começou nivel " + index.ToString());
		enemySpawner.enemyCount = 0;
		Queue<Enemy> enemyQueue = new Queue<Enemy> ();
		LevelInfo level = levels [index];

		foreach (LevelEnemies e in level.enemies) {
			for (int i = 0; i < e.quantity; i++) {
				enemyQueue.Enqueue (e.enemyPrefab);
			}
		}

		currentLevel = index;

		StartCoroutine (RunLevel(enemyQueue, level.enemySpawnInterval));
	}

	void EndLevel(){
		Debug.Log ("Acabou nivel " + currentLevel.ToString ());
		UIManager.instance.EndLevel ();
	}

	public void EndGame(){
		UIManager.instance.EndGame ();
		StopCoroutine ("RunLevel");
	}

	public void ResetGame(){
		enemySpawner.Reset ();
	}

	IEnumerator RunLevel(Queue<Enemy> enemyQueue, float spawnInterval){
		while (enemyQueue.Count > 0) {
			enemySpawner.Spawn (enemyQueue.Dequeue ());
			yield return new WaitForSeconds (spawnInterval);
		}

		while (enemySpawner.enemyCount > 0) {
			yield return null;
		}

		EndLevel ();
		yield return null;

	}
}
