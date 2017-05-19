﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour {

	[Header("References")]
	public Castle castle;

	[Header("Prices")]
	public int archerPrice;
	public int builderPrice;
	public int wallsPrice;
	public int fixPrice;
	public int improvePrice;

	[Header("Upgrade Values")]
	public int fixAmount;
	public int wallAmount;
	public int improveAmount;


	bool SpendGold(int value){
		return GoldManager.instance.SpendGold (value);
	}

	public void FixCastle(){
		if (SpendGold (fixPrice)) {
			castle.Heal (fixAmount);
		}
	}

	public void ImproveCastle(){
		if (SpendGold (improvePrice)) {
			castle.ImproveCastle (improveAmount);
		}
	}

	public void RaiseWalls(){
		if (SpendGold (wallsPrice)) {
			castle.RaiseWalls (wallAmount);
		}
	}

	public void HireArcher(){
		if (SpendGold (archerPrice)) {
			castle.HireArcher();
		}
	}

	public void HireBuilder(){
		if (SpendGold (builderPrice)) {
			castle.HireBuilder();
		}
	}


}
