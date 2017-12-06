using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayExpertAdvice : MonoBehaviour {

	bool playing = false;
	void Start () {
			GetComponent<Button>().onClick.AddListener(() => { StartCoroutine(PlaySound()); });
	}

	IEnumerator PlaySound() {
		if (!playing) {
			playing = true;
			yield return SoundManager.PlayRandom(this, "ExpertAdvice", Vector3.zero, 0.5f);
			playing = false;
		}
	}
}
