using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldUIDisplayer : MonoBehaviour {

	private int _lastGoldCount = 0;
	void Start () {
		_lastGoldCount = PlayerStats.GoldCount;
	}

	void Update() {
		if (_lastGoldCount != PlayerStats.GoldCount) {
			DisplayGolds(PlayerStats.GoldCount);
		}

		_lastGoldCount = PlayerStats.GoldCount;
	}


	void DisplayGolds(int amount) {
		if (amount > 5) {
			amount = 5;
		}
		// disable all pens
		for(int i = transform.childCount-1; i >= 0 ; i--) {
			transform.GetChild(i).gameObject.SetActive(false);
		}

		for (int i = 0; i < amount; i++)
			transform.GetChild(i).gameObject.SetActive(true);
	}
}
