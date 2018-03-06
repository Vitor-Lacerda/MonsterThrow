using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteChanger : MonoBehaviour {

	public Sprite[] sprites;


	// Use this for initialization
	void Start () {
		ChangeSprite ();
	}

	void OnEnable(){
		ChangeSprite ();
	}

	void ChangeSprite(){
		SpriteRenderer sr = this.GetComponent<SpriteRenderer> ();
		sr.sprite = sprites [Random.Range (0, sprites.Length)];
	}
}
