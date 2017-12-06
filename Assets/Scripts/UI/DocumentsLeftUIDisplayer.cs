using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentsLeftUIDisplayer : MonoBehaviour {

	int lastDeposited = 0;
	public GameObject ImageObject;
	void Start () {
		for (int i = 0; i < PlayerStats.DocumentsWinCriterea; i++) {
			IncreaseDocumentCount();
		}
	}
	
	void LateUpdate() {
		if (lastDeposited != PlayerStats.DocumentsDepositedCount) {
			var toRemove = PlayerStats.DocumentsDepositedCount - lastDeposited;
			Remove(toRemove);
		}

		lastDeposited = PlayerStats.DocumentsDepositedCount;
	}

	void IncreaseDocumentCount() {
		Instantiate(ImageObject, transform);
	}

	void Remove(int num) {
		for (int i = 0; i < num; i++) {
			var index = 1+i;
			Destroy(transform.GetChild(transform.childCount - index).gameObject);
		}
	}
}
