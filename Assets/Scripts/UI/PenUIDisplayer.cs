using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PenUIDisplayer : MonoBehaviour {
	private int _lastPenCount = 0;
	private int _lastPaintUsed = 0;

	[Range(0.1f, 1f)]
	public float PenDecreaseValue = 0.1f;
	void Start () {
		DisplayPens(_lastPenCount);
	}

	void Update() {
		if (_lastPenCount != PlayerStats.PenCount) {
			DisplayPens(PlayerStats.PenCount);
		}
		if (_lastPaintUsed != PaintManager.PaintUsed()) {
			DecreaseInc();
		}

		_lastPaintUsed = PaintManager.PaintUsed();
		_lastPenCount = PlayerStats.PenCount;
	}


	void DisplayPens(int amount) {
		if (amount > 5) {
			amount = 5;
		}
		// disable all pens
		for(int i = transform.childCount-1; i >= 0 ; i--) {
			transform.GetChild(i).gameObject.SetActive(false);
		}

		// enable just the right amount :ok_hand:
		for (int i = 0; i < amount; i++) {
			transform.GetChild(i).gameObject.SetActive(true);
			Debug.Log($"enabling {transform.GetChild(i).gameObject.name}");
		}
	}

	public void DecreaseInc() {
		for(int i = transform.childCount-1; i >= 0 ; i--) {
			var childTran = transform.GetChild(i);
			if (childTran.gameObject.activeSelf) {
				var bar = childTran.Find("PenBar").gameObject;
				var script = bar.GetComponent<PenBar>();
				if (script.penLevel > 0) {
					script.penLevel -= PenDecreaseValue * Time.deltaTime;
					if (script.penLevel <= 0) {
						PlayerStats.PenCount--;
					}
					return;
				}
			}
		}
		FadeManager.FadeOut(this, () => {
			SceneManager.LoadScene("40_EndScreen_Burnout");
		});
	}
}
