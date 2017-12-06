using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnykeyNextScene : MonoBehaviour {
	private bool _fadeInDone = false;
	void Start() {
		FadeManager.FadeIn(this, ()=>{_fadeInDone = true;}, 1);
	}
	private bool _notRunOnce = true;
	void Update () {
		if (_fadeInDone && Input.anyKeyDown && _notRunOnce) {
			FadeManager.FadeOut(this, () => {
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			}, 0.5f);
		}
	}
}
