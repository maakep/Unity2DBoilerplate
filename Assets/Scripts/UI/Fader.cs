using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script belongs on a UI GameObject with an Image component. Set the Rect Transform to Strech in achors X and Y and
/// set Left, Top, Right and Bottom to 0; filling the screen with the image.
/// </summary>
public class FadeManager {
	private Image _fadeImage;
	private GameManager gameManager;
	private Color _transparent = new Color(0, 0, 0, 0);
	public FadeManager (GameManager gm) {
		gameManager = gm;
		_fadeImage = GameObject.Find("Fader").GetComponent<Image>();

		#if UNITY_EDITOR
		if (_fadeImage == null) {
			Debug.LogError(gm.name + " couldn't find Image component for the fader object");
		}
		#endif
	}

	// The "if no color is set, default to black"-overload
	public void FadeOut(Action doneCallback, float fadeSpeed = 1) {
		FadeOut(doneCallback, Color.black, fadeSpeed);
	}
	public void FadeOut(Action doneCallback, Color fadeOutColor, float fadeSpeed = 1) {
		gameManager.StartCoroutine(_FadeOut(doneCallback, fadeOutColor, fadeSpeed));
	}
	private IEnumerator _FadeOut(Action doneCallback, Color fadeOutColor, float fadeSpeed) {
		var ticks = 0f;
		do {
			_fadeImage.color = Color.Lerp(_transparent, fadeOutColor, ticks);
			yield return new WaitForEndOfFrame();
			ticks += Time.deltaTime * fadeSpeed;
		} while (_fadeImage.color != fadeOutColor);

		doneCallback();
	}

	public void FadeIn(Action doneCallback, float fadeSpeed = 1) {
		FadeIn(doneCallback, Color.black, fadeSpeed);
	}
	public void FadeIn(Action doneCallback, Color fadeOutColor, float fadeSpeed = 1) {
		gameManager.StartCoroutine(_FadeIn(doneCallback, fadeOutColor, fadeSpeed));
	}
	private IEnumerator _FadeIn(Action doneCallback, Color fadeOutColor, float fadeSpeed) {
		
		var ticks = 0f;
		do {
			_fadeImage.color = Color.Lerp(fadeOutColor, _transparent, ticks);
			yield return new WaitForEndOfFrame();
			ticks += Time.deltaTime * fadeSpeed;
		} while (_fadeImage.color != _transparent);
		doneCallback();
	}

	public bool Activate() {
		Debug.Log("Activate!");
		FadeOut(()=>{});
		return true;
	}
}
