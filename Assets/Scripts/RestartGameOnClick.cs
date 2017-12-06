using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGameOnClick : MonoBehaviour {

	private bool _fadeInDone = false;
	void Start() {
		FadeManager.FadeIn(this, ()=>{_fadeInDone = true;}, 1);
	}
	
	private bool _notRunOnce = true;
	void Update () {
		if (_fadeInDone && Input.anyKeyDown && _notRunOnce) {
			FadeManager.FadeOut(this, () => {
				SceneManager.LoadScene("0_TitleScreen");
			}, 0.5f);
		}
	}
}
