using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	public float moveSpeed;
	IDamageable target;
	Vector3 targetPosition;
	Vector3 travelDirection;
	float damage;

	public void Init(IDamageable t, float dmg){
		target = t;
		MonoBehaviour mb = t as MonoBehaviour;
		targetPosition = mb.transform.position;
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
		if (other.GetComponent<IDamageable>() == target) {
			target.Damage (damage);
			gameObject.SetActive (false);
		}
	}
}
