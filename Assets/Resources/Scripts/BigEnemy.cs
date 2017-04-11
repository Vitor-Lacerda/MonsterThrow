using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemy : Enemy {


	protected bool reeling = false;


	void Awake(){
		
	}

	void Start () {
		Init ();
	}

	public override void Init ()
	{

		myCollider = this.GetComponent<Collider2D> ();
		rigidBody = this.GetComponent<Rigidbody2D> ();
		animator = this.GetComponent<Animator> ();
		currentHealth = maxHealth;
		moveSpeed = startMovespeed;
		reeling = false;	
		myCollider.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (!reeling && currentHealth > 0) {
			rigidBody.velocity = new Vector2 (moveSpeed, 0);
		} else {
			rigidBody.velocity = Vector2.zero;
		}
	}


	protected override void TakeDamage(float damage){
		Debug.Log (damage);
		currentHealth -= damage;
		if (currentHealth <= 0) {
			animator.SetBool ("Dying", true);
		} else {
			animator.SetBool ("Reeling", true);
		}
	}

	void OnStartHit(){
		reeling = true;
	}

	void OnEndHit(){
		reeling = false;
		animator.SetBool ("Reeling", false);
	}

	protected override void OnStartDie(){
		myCollider.enabled = false;
	}

	protected override void OnEndDie(){
		gameObject.SetActive (false);
	}


	void OnCollisionEnter2D(Collision2D col){
		if (!reeling && col.rigidbody!=null) {
			TakeDamage (col.rigidbody.velocity.magnitude);
		}
	}
}
