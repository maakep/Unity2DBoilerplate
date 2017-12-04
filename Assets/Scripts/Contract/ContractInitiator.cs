using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractInitiator : MonoBehaviour {

	Sprite[] contracts;
	void Start () {
		contracts = Resources.LoadAll<Sprite>("Sprites/Contracts");
		GenerateNew();
	}

	public void GenerateNew() {
		var chosen = Random.Range(0, contracts.Length);
		GetComponent<SpriteRenderer>().sprite = contracts[chosen];
	}
}
