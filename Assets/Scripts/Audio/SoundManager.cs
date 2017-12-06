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
public static class SoundManager
{
  static private AudioClip[] _clips;
  static public AudioClip IncorrectClipName;
  static SoundManager() {
    _clips = Resources.LoadAll<AudioClip>("Audio");
    IncorrectClipName = Resources.Load<AudioClip>("Audio/error");
  }

  private static AudioClip GetClip(string clip) {
    foreach (var c in _clips) {
      if (c.name == clip) {
        return c;
      }
    }
    return IncorrectClipName;
  }

  public static Coroutine Play(MonoBehaviour anyScript, string clip, Vector3 position){return anyScript.StartCoroutine(_Play(clip, position, 1.0f));}
  public static Coroutine Play(MonoBehaviour anyScript, string clip, Vector3 position, float volume){return anyScript.StartCoroutine(_Play(clip, position, volume));}
  private static IEnumerator _Play(string clip, Vector3 position, float volume)
  {
    var newObj = new GameObject("Soundplayer_" + clip, typeof(AudioSource));
    newObj.transform.position = position;
    var aus = newObj.GetComponent<AudioSource>();
    var audio = GetClip(clip);
    aus.volume = volume;
    aus.PlayOneShot(audio);
    yield return new WaitForSeconds(audio.length + 1);
    Object.Destroy(newObj);
  }

///<summary>
/// Plays a sound based on a folder in Resources. They load once, and is then cached to be reused. 
///</summary>
  public static Coroutine PlayRandom(MonoBehaviour anyScript, string folder, Vector3 position){return anyScript.StartCoroutine(_PlayRandom(folder, position, 1.0f));}
  public static Coroutine PlayRandom(MonoBehaviour anyScript, string folder, Vector3 position, float volume){return anyScript.StartCoroutine(_PlayRandom(folder, position, volume));}
  private static IEnumerator _PlayRandom(string folder, Vector3 position, float volume)
  {
    var sounds = TryGetFromRandomCache(folder);
    var newObj = new GameObject("SoundplayerRandom_" + folder, typeof(AudioSource));
    newObj.transform.position = position;
    var aus = newObj.GetComponent<AudioSource>();
    AudioClip audio = sounds[Random.Range(0, sounds.Length)];
    aus.volume = volume;
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
  private static List<RandomCache> _randomCache = new List<RandomCache>();
  private static AudioClip[] TryGetFromRandomCache(string folder) {
    RandomCache soundList = _randomCache.FirstOrDefault(x => x.folderName == folder);
    AudioClip[] sounds = null;

    if (soundList != null) {
      sounds = soundList.clips;
    }

    if (sounds == null) {
      sounds = Resources.LoadAll<AudioClip>("Audio/" + folder);
    }

    if (sounds.Length > 0) {
      _randomCache.Add(new RandomCache(folder, sounds));
    } else {
      sounds = new AudioClip[]{ IncorrectClipName };
    }

    return sounds;
  }
}