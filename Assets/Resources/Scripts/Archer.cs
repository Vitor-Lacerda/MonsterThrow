using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour {

	public float range = 3f;
	public float damage = 1f;
	public float fireCooldown = 1f;
	public Arrow arrowPrefab;

	Enemy target;
	float shootTime = 0f;

	void Start () {
		target = null;	
	}

	void Update () {
		if (target == null) {
			FindTarget ();
		} else {
			if (target.gameObject.activeSelf) {
				if (Time.time - shootTime > fireCooldown) {
					Shoot ();
				}
			} else {
				target = null;
			}
		}
	}

	void FindTarget(){
		Collider2D col = Physics2D.OverlapCircle (transform.position, range, LayerMask.GetMask("Enemy"));
		if (col != null) {
			target = col.GetComponent<Enemy> ();
		}
	}

	void Shoot(){
		shootTime = Time.time;

		Arrow newArrow = ObjectPooler.Spawn<Arrow> (arrowPrefab, transform.position);
		newArrow.Init (target, damage);

	}
}
