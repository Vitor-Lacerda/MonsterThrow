using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public static UIManager instance;

	public Image healthBar;
	public Image wallsBar;

	public GameObject endLevelPanel;
	public GameObject defeatPanel;

	void Awake(){
		if (instance == null) {
			instance = this;
		}
	}

	public void SetHealthBar(float proportion){
		float p = Mathf.Clamp (proportion, 0, 1);
		healthBar.rectTransform.localScale = new Vector2 (p, healthBar.rectTransform.localScale.y);
	}

	public void SetWallsBar(float proportion){
		float p = Mathf.Clamp (proportion, 0, 1);
		wallsBar.rectTransform.localScale = new Vector2 (p, wallsBar.rectTransform.localScale.y);
	}

	public void EndLevel(){
		endLevelPanel.SetActive (true);
	}

	public void EndGame(){
		endLevelPanel.SetActive (false);
		defeatPanel.SetActive (true);
	}



}
