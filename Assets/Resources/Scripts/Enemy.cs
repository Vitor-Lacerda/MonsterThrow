﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	protected enum EnemyStates
	{
		Walking,
		Dragging,
		Falling,
		Standing,
		Dead
	}


	public float maxHealth = 10;
	public float startMovespeed = 10;
	public float groundCheckRange = 0.5f;
	public float intensity, drag;

	protected EnemyStates currentState;
	protected Rigidbody2D rigidBody;
	protected Animator animator;
	protected Collider2D myCollider;

	protected float currentHealth = 0;
	protected float moveSpeed = 0;

	void Awake(){
		
	}
		
	void Start () {
		Init ();
	}

	public virtual void Init(){
		rigidBody = this.GetComponent<Rigidbody2D> ();
		animator = this.GetComponent<Animator> ();
		myCollider = this.GetComponent<Collider2D> ();
		currentHealth = maxHealth;
		moveSpeed = startMovespeed;
		currentState = EnemyStates.Walking;
		animator.SetFloat ("Health", currentHealth);
		myCollider.enabled = true;
	}

	void Update () {
		bool grounded = IsGrounded ();
		animator.SetBool ("Grounded", grounded);
		animator.SetFloat ("YVelocity", rigidBody.velocity.y);

		if (currentState == EnemyStates.Walking) {
			if (grounded) {
				rigidBody.velocity = new Vector2 (moveSpeed, 0);
			} else {
				currentState = EnemyStates.Falling;
			}
		} 

		else if (currentState == EnemyStates.Falling) {
			if (grounded) {
				//currentState = EnemyStates.Walking;
				Fall();
			}
		}

		myCollider.enabled = !(currentState == EnemyStates.Dragging);


	}

	void FixedUpdate(){
		if (currentState == EnemyStates.Dragging) {
			Drag ();
		}
	}

	//Trocar depois
	/*
	void OnMouseDown(){
		Grab ();

	}

	void OnMouseUp(){
		Release ();
	}
	*/

	public void Grab(){
		if (currentState == EnemyStates.Dead) {
			return;
		}
		currentState = EnemyStates.Dragging;
		animator.SetBool ("Dragging", true);
	}

	public void Release(){
		currentState = EnemyStates.Falling;
		animator.SetBool ("Dragging", false);
	}

	protected void Drag(){
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 vel = mousePos - (Vector2)transform.position;
		vel.Normalize ();
		rigidBody.velocity = Vector2.Lerp(rigidBody.velocity, vel*intensity, drag*Time.deltaTime); 
	}

	protected void Fall(){
		TakeDamage (rigidBody.velocity.magnitude);
	}

	/*A maquina de estados do animador cuida pra ver se levanta ou morre
	 * e volta pro estado correto depois*/
	protected void OnStartStandUp(){
		currentState = EnemyStates.Standing;
	}

	protected void OnEndStandUp(){
		currentState = EnemyStates.Walking;
	}



	protected virtual void OnStartDie(){
		currentState = EnemyStates.Dead;
	}

	protected virtual void OnEndDie(){
		gameObject.SetActive (false);
	}

	protected virtual void TakeDamage(float damage){
		currentHealth -= damage;
		animator.SetFloat ("Health", currentHealth);
	}

	protected bool IsGrounded(){
		RaycastHit2D hit;
		Debug.DrawRay (transform.position, Vector2.down * (groundCheckRange), Color.white);
		hit = Physics2D.Raycast (transform.position, Vector2.down, groundCheckRange, LayerMask.GetMask ("Chao"));
		if (hit.collider != null) {
			return true;
		}

		return false;
	}

	/*
	void OnCollisionEnter2D(Collision2D col){
		TakeDamage (col.relativeVelocity.sqrMagnitude);
	}*/

}
