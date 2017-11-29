using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// The IncorrectClipName needs to be set manually, it will be played if a clip can't be found.
/// PlayRandom() plays a sound based on a folder in Resources. They load once and is then cached to be reused.
/// To play a random footstep sound, put all footstep sounds in Resources/Footsteps/ and call PlayRandom("Footsteps", this.transform.position);
/// To play one specific clip, call Play("ClipName")
///</summary>
public class SoundManager
{
  private AudioClip[] _clips;
  private GameManager _gameManager;
  public AudioClip IncorrectClipName;
  public SoundManager(GameManager gm) {
    _gameManager = gm;
    _clips = Resources.LoadAll<AudioClip>("Audio");
  }

  private AudioClip GetClip(string clip) {
    foreach (var c in _clips) {
      if (c.name == clip) {
        return c;
      }
    }
    return IncorrectClipName;
  }


  public void Play(string clip, Vector3 position){_gameManager.StartCoroutine(_Play(clip, position));}
  private IEnumerator _Play(string clip, Vector3 position)
  {
    var newObj = new GameObject("Soundplayer_" + clip, typeof(AudioSource));
    newObj.transform.position = position;
    var aus = newObj.GetComponent<AudioSource>();
    var audio = GetClip(clip);
    aus.PlayOneShot(audio);
    yield return new WaitForSeconds(audio.length + 1);
    Object.Destroy(newObj);
  }

///<summary>
/// Plays a sound based on a folder in Resources. They load once, and is then cached to be reused. 
///</summary>
  public void PlayRandom(string folder, Vector3 position){_gameManager.StartCoroutine(_PlayRandom(folder, position));}
  private IEnumerator _PlayRandom(string folder, Vector3 position)
  {
    var sounds = TryGetFromRandomCache(folder);
    var newObj = new GameObject("SoundplayerRandom_" + folder, typeof(AudioSource));
    newObj.transform.position = position;
    var aus = newObj.GetComponent<AudioSource>();
    aus.playOnAwake = false;
    AudioClip audio = sounds[Random.Range(0, sounds.Length)];
    Debug.Log(audio.name);
    aus.PlayOneShot(audio);
    yield return new WaitForSeconds(audio.length + 1);
    Object.Destroy(newObj);
  }

  private class RandomCache {
    public RandomCache(string folderName, AudioClip[] clips) {
      this.folderName = folderName;
      this.clips = clips;
    }
    public string folderName;
    public AudioClip[] clips;
  }
  private List<RandomCache> _randomCache = new List<RandomCache>();
  private AudioClip[] TryGetFromRandomCache(string folder) {
    RandomCache soundList = _randomCache.FirstOrDefault(x => x.folderName == folder);
    AudioClip[] sounds = null;

    if (soundList != null) {
      sounds = soundList.clips;
    }

    if (sounds == null) {
      sounds = Resources.LoadAll<AudioClip>("Audio/" + folder);
      Debug.Log("Loading from resources: " + sounds.Count());
    }

    if (sounds.Length > 0) {
      _randomCache.Add(new RandomCache(folder, sounds));
    } else {
      sounds = new AudioClip[]{ IncorrectClipName };
    }

    return sounds;
  }
}