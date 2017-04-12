using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour {

	public float maxHealth = 100;

	float currentHealth;

	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
	}

	public void Damage(float value){
		currentHealth -= value;
		UIManager.instance.SetHealthBar (currentHealth / maxHealth);
	}

}
