using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour {

	public Rigidbody2D r;
	public float force = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.D)) {
			r.velocity = new Vector2 (force, 0);
		}	
		if (Input.GetKeyDown (KeyCode.S)) {
			r.velocity = new Vector2 (-force, 0);
		}
	}
}
