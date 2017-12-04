using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountUpperTimer : MonoBehaviour {

	Text timer;
	int time = 0;
	void Start () {
		timer = GetComponent<Text>();
		StartCoroutine(CountUp());
	}

	IEnumerator CountUp() {
		while (true) {
			time++;
			timer.text = time.ToString();
			yield return new WaitForSeconds(1f);
		}
	}
}
