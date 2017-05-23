using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour {

	public static GoldManager instance;

	public int currentGold {
		get;
		protected set;
	}


	void Awake(){
		if (instance == null) {
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
		currentGold = 0;
		UIManager.instance.goldText.text = currentGold.ToString ();

	}

	public void EarnGold(int value){
		currentGold += value;
		UIManager.instance.goldText.text = currentGold.ToString ();
	}

	public bool SpendGold(int value){
		if (currentGold < value) {
			return false;
		}
		currentGold -= value;
		UIManager.instance.goldText.text = currentGold.ToString ();
		return true;
	}


}
