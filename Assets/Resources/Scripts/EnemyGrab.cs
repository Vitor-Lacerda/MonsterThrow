using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrab : MonoBehaviour {

	public Enemy enemy;

	void OnMouseDown(){
		enemy.Grab ();

	}

	void OnMouseUp(){
		enemy.Release ();
	}
}
