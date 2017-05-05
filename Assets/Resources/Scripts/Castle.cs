using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour {
	[Header("Castle")]
	public float maxHealth = 100;
	public float maxWalls = 100;

	[Header("Prefabs")]
	public Archer archerPrefab;
	public Builder builderPrefab;

	[Header("Positions")]
	public Transform[] archerPositions;
	public Transform[] builderPositions;

	float _walls;

	float walls{
		get{
			return _walls;
		}
		set{
			_walls = value;
			UIManager.instance.SetWallsBar (_walls / maxWalls);
		}
	}


	float _currentHealth;

	float currentHealth{
		get{
			return _currentHealth;
		}

		set{
			_currentHealth = value;
			UIManager.instance.SetHealthBar (_currentHealth / maxHealth);

		}

	}

	List<Archer> archers;
	List<Builder> builders;

	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
		archers = new List<Archer> ();
		builders = new List<Builder> ();
	}
		
	public void Damage(float value){
		if (walls > 0) {
			walls -= value;
		} else {
			currentHealth -= value;
		}
	}

	public void Fix(float value){
		currentHealth += value;
		currentHealth = Mathf.Clamp (currentHealth, 0, maxHealth);
	}

	public void ImproveCastle(float value){
		maxHealth += value;
		currentHealth += value;
	}

	public void RaiseWalls(float value){
		walls += value;
		//Efeito visual
	}

	public void HireArcher(){
		int count = archers.Count;
		if (count < archerPositions.Length) {
			Archer newArcher = ObjectPooler.Spawn<Archer> (archerPrefab, archerPositions [count].position) as Archer;
			archers.Add (newArcher);
		}
	}

	public void HireBuilder(){
		int count = builders.Count;
		if (count < builderPositions.Length) {
			Builder newBuilder = ObjectPooler.Spawn<Builder> (builderPrefab, builderPositions [count].position) as Builder;
			newBuilder.castle = this;
			builders.Add (newBuilder);
		}
	}
}
