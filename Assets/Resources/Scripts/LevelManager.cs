using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	[System.Serializable]
	public struct EnemyConfig //Tinha que virar um arquivo config depois.
	{
		public Enemy enemyPrefab;
		public int firstLevel;
		public float minPercentage;
		public float maxPercentage;
	}

	[System.Serializable]
	public struct BossLevelInfo
	{
		public Enemy boss;
		public int level;
		public Enemy[] minions;
	}

	[Header("Properties")]
	public float enemySpawnInterval;
	public int baseEnemyAmount = 10;
	public int enemiesPerLevel = 5;

	[Header("Info")]
	public List<EnemyConfig> enemyConfig; //Tem que resgatar de um arquivo depois?
	public List<BossLevelInfo> bossesConfig;




	int currentLevel = 0;

	void Start () {
	}

	void Update () {
		
	}

	public void NextLevel(){
		MakeLevelList (currentLevel + 1);
	}

	void MakeLevelList(int level){
		Debug.Log ("Começou nivel " + level.ToString());
		EnemySpawner.instance.enemyCount = 0;
		List<Enemy> enemyQueue = new List<Enemy> ();
		List<EnemyConfig> possibleEnemies = enemyConfig.Where (x => x.firstLevel <= level).OrderByDescending (enemy => enemy.firstLevel).ToList ();
		int levelEnemyCount = baseEnemyAmount + enemiesPerLevel * level;
		int c = 0;
		float remainingPercent = 100;
		while (c < possibleEnemies.Count && remainingPercent > 0) {
			if (remainingPercent >= possibleEnemies [c].minPercentage) {
				float high = Mathf.Min (remainingPercent, possibleEnemies [c].maxPercentage);
				float p = Random.Range (possibleEnemies [c].minPercentage, high);
				int amount = Mathf.FloorToInt (levelEnemyCount * p / 100);
				for (int i = 0; i < amount; i++) {
					enemyQueue.Add (possibleEnemies [c].enemyPrefab);
				}
				remainingPercent -= p;
			}
			c++;
		}
		if (remainingPercent > 0) {
			int amount = Mathf.FloorToInt (levelEnemyCount * remainingPercent / 100);
			for (int i = 0; i < amount; i++) {
				enemyQueue.Add (possibleEnemies.Last().enemyPrefab);
			}
			remainingPercent = 0;
		}




		StartCoroutine (RunLevel(enemyQueue, enemySpawnInterval, level));
		currentLevel = level;

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
		EnemySpawner.instance.Reset ();
	}

	IEnumerator RunLevel(List<Enemy> enemyQueue, float spawnInterval, int level){
		int c = 0;
		List<Enemy> shuffledList = enemyQueue.OrderBy (x => Random.value).ToList ();
		while (c < shuffledList.Count) {
			Enemy newenemy = shuffledList [c];
			EnemySpawner.instance.Spawn (newenemy);
			c++;
			yield return new WaitForSeconds (spawnInterval);
		}

		while (EnemySpawner.instance.bigEnemyCount > 0) {
			Enemy newenemy = enemyQueue [Random.Range(0, shuffledList.Count)];
			EnemySpawner.instance.Spawn (newenemy);
			yield return new WaitForSeconds (spawnInterval);
		}

		bool b = bossesConfig.Any (x => x.level == level);
		if (b) {
			BossLevelInfo bossInfo = bossesConfig.First (x => x.level == level);
			Debug.Log ("BOSS");
			EnemySpawner.instance.Spawn (bossInfo.boss);
			yield return new WaitForSeconds (spawnInterval);
			while (EnemySpawner.instance.bigEnemyCount > 0) {
				EnemySpawner.instance.Spawn (bossInfo.minions [Random.Range (0, bossInfo.minions.Length)]);
				yield return new WaitForSeconds (spawnInterval);

			}
		}


		while (EnemySpawner.instance.enemyCount > 0) {
			yield return null;
		}

		EndLevel ();
		yield return null;

	}
}
