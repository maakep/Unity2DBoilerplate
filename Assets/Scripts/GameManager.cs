using UnityEngine;
public class GameManager : MonoBehaviour {
  public ParticleManager ParticleManager;
  public FadeManager FadeManager;
  public SoundManager SoundManager;
  void Start() {
    ParticleManager = new ParticleManager();
    FadeManager = new FadeManager(this);
    SoundManager = new SoundManager(this);
  }
}