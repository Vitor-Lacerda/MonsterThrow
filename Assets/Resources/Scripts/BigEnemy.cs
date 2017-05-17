using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemy : Enemy {


	//protected bool reeling = false;


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
		currentState = EnemyStates.Walking;
		myCollider.enabled = true;
		transform.position = new Vector3 (transform.position.x, -3.91f,  -3.91f);
		startHeight = transform.position.y;

	}
	
	// Update is called once per frame
	void Update () {

		bool grounded = IsGrounded ();

		animator.SetBool ("Attacking", (currentState == EnemyStates.Attacking));


		//CheckAttack ();
		if (currentState == EnemyStates.Walking && currentHealth > 0) {
			rigidBody.velocity = new Vector2 (moveSpeed, 0);
		} 
		else {
			rigidBody.velocity = Vector2.zero;
		}

		rigidBody.gravityScale = grounded ? 0f : 1f;

	}


	public override void Damage(float damage){
		Debug.Log (damage);
		currentHealth -= damage;
		if (currentHealth <= 0) {
			animator.SetBool ("Dying", true);
		} else {
			animator.SetBool ("Reeling", true);
		}
	}

	void OnStartHit(){
		//reeling = true;
		currentState = EnemyStates.Standing;
	}

	void OnEndHit(){
		//reeling = false;
		//currentState = EnemyStates.Walking;
		CheckAttack();
		animator.SetBool ("Reeling", false);
	}

	protected override void OnStartDie(){
		base.OnStartDie ();
		myCollider.enabled = false;
	}

	protected override void OnEndDie ()
	{
		base.OnEndDie ();
		GameObject.FindObjectOfType<EnemySpawner> ().bigEnemyCount--;

	}



	void OnCollisionEnter2D(Collision2D col){
		if (currentState != EnemyStates.Standing && col.rigidbody!=null) {
			Damage (col.rigidbody.velocity.magnitude);
		}
	}
}
