using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour {

	public float fixAmount = 3f;
	public float fixRate = 1f;

	public Castle castle;

	float fixTime = 0;

	void Start () {
		
	}

	void Update () {
		if (Time.time - fixTime >= fixRate) {
			castle.Heal (fixAmount);
			fixTime = Time.time;
		}
	}
}
