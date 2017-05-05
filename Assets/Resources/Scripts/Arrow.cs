using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	public float moveSpeed;
	Enemy target;
	Vector3 targetPosition;
	Vector3 travelDirection;
	float damage;

	public void Init(Enemy t, float dmg){
		target = t;
		targetPosition = t.transform.position;
		travelDirection = targetPosition - transform.position;
		damage = dmg;
		gameObject.SetActive (true);
	}

	void Update(){
		transform.position += travelDirection.normalized * moveSpeed * Time.deltaTime;

		Vector2 viewportPos = Camera.main.WorldToViewportPoint (transform.position);
		if (viewportPos.x <= -0.5f || viewportPos.x >= 1.5f || viewportPos.y <= -0.5f || viewportPos.y >= 1.5f) {
			gameObject.SetActive (false);
		}

	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<Enemy>() != null) {
			other.GetComponent<Enemy> ().TakeDamage (damage);
			gameObject.SetActive (false);
		}
	}
}
