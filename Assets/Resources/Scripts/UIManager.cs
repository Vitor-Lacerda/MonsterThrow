using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public static UIManager instance;

	[Header("References")]
	public UpgradeManager upgradeManager;
	public Castle castle;

	[Header("Health")]
	public Image healthBar;
	public Image wallsBar;

	[Header("Currency")]
	public Text goldText;

	[Header("End level")]
	public GameObject endLevelPanel;
	public Button raiseWallsButton;
	public Text raiseWallsPrice;
	public Button improveCastleButton;
	public Text improveCastlePrice;
	public Button fixCastleButton;
	public Text fixCastlePrice;
	public Button hireArcherButton;
	public Text hireArcherPrice;
	public Button hireBuilderButton;
	public Text hireBuilderPrice;


	[Header("Defeat")]
	public GameObject defeatPanel;

	void Awake(){
		if (instance == null) {
			instance = this;
		}
	}

	void Start(){
		EndLevel ();
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
		UpdateUpgradeButtons ();
	}

	public void EndGame(){
		endLevelPanel.SetActive (false);
		defeatPanel.SetActive (true);
	}

	public void UpdateUpgradeButtons(){
		raiseWallsButton.interactable = GoldManager.instance.currentGold > upgradeManager.wallsPrice;
		raiseWallsPrice.text = upgradeManager.wallsPrice.ToString();

		improveCastleButton.interactable = GoldManager.instance.currentGold > upgradeManager.improvePrice;
		improveCastlePrice.text = upgradeManager.improvePrice.ToString();

		fixCastleButton.interactable = GoldManager.instance.currentGold > upgradeManager.fixPrice;
		fixCastlePrice.text = upgradeManager.fixPrice.ToString();

		hireArcherButton.interactable = (GoldManager.instance.currentGold > upgradeManager.archerPrice && castle.archers.Count < castle.archerPositions.Length);
		hireArcherPrice.text = upgradeManager.archerPrice.ToString();

		hireBuilderButton.interactable = (GoldManager.instance.currentGold > upgradeManager.builderPrice && castle.builders.Count < castle.builderPositions.Length);
		hireBuilderPrice.text = upgradeManager.builderPrice.ToString();



	}



}
