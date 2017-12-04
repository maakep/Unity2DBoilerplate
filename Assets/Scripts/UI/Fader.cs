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
	private static Image _fadeImage;
	private static Color _transparent = new Color(0, 0, 0, 0);
	private static bool _isFading = false;
	static FadeManager () {
		_fadeImage = GameObject.Find("Fader").GetComponent<Image>();

		#if UNITY_EDITOR
		if (_fadeImage == null) {
			Debug.LogError("couldn't find Image component for the fader object");
		}
		#endif
	}

	// The "if no color is set, default to black"-overload
	public static void FadeOut(MonoBehaviour anyScript, Action doneCallback, float fadeSpeed = 1) {
		FadeOut(anyScript, doneCallback, Color.black, fadeSpeed);
	}
	public static void FadeOut(MonoBehaviour anyScript, Action doneCallback, Color fadeOutColor, float fadeSpeed = 1) {
		if (_isFading)
			return;
		anyScript.StartCoroutine(_FadeOut(doneCallback, fadeOutColor, fadeSpeed));
	}
	private static IEnumerator _FadeOut(Action doneCallback, Color fadeOutColor, float fadeSpeed) {
		_isFading = true;
		_fadeImage = GameObject.Find("Fader").GetComponent<Image>();
		var ticks = 0f;
		do {
			_fadeImage.color = Color.Lerp(_transparent, fadeOutColor, ticks);
			yield return new WaitForEndOfFrame();
			ticks += Time.deltaTime * fadeSpeed;
		} while (_fadeImage.color != fadeOutColor);
		_isFading = false;
		doneCallback();
	}

	public static void FadeIn(MonoBehaviour anyScript, Action doneCallback, float fadeSpeed = 1) {
		FadeIn(anyScript, doneCallback, Color.black, fadeSpeed);
	}
	public static void FadeIn(MonoBehaviour anyScript, Action doneCallback, Color fadeOutColor, float fadeSpeed = 1) {
		if (_isFading)
			return;
		anyScript.StartCoroutine(_FadeIn(doneCallback, fadeOutColor, fadeSpeed));
	}
	private static IEnumerator _FadeIn(Action doneCallback, Color fadeOutColor, float fadeSpeed) {
		_fadeImage = GameObject.Find("Fader").GetComponent<Image>();
		_isFading = true;
		var ticks = 0f;
		do {
			_fadeImage.color = Color.Lerp(fadeOutColor, _transparent, ticks);
			yield return new WaitForEndOfFrame();
			ticks += Time.deltaTime * fadeSpeed;
		} while (_fadeImage.color != _transparent);
		_isFading = false;
		doneCallback();
	}
}
