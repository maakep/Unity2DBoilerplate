using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAudioController : MonoBehaviour {
	private Renderer _renderer; 
	private AudioSource _audioSource;
	private bool _audio;

	// Use this for initialization
	void Start () {
		_renderer = GetComponent<Renderer>();
		_audioSource = GetComponent<AudioSource>();
		_audio = _renderer.isVisible;
		if(_audio)
			_audioSource.Play();
		else
			_audioSource.Stop();
	}
	
	// Update is called once per frame
	void Update () {
		if(_renderer.isVisible && !_audio) {
			_audio = true;
			_audioSource.Play();
		} else if(!_renderer.isVisible && _audio) {
			_audio = false;
			_audioSource.Stop();
		}
	}
}
