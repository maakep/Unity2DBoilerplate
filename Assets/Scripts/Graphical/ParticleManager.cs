using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class ParticleManager
{
  private static List<GameObject> _particles = new List<GameObject>();
  static ParticleManager() {
    _particles = Resources.LoadAll<GameObject>("Particles").ToList();
    Debug.Log("Particles: " + _particles.Count());
  }

  public static GameObject GetParticle(string particle) {
    return _particles.FirstOrDefault(x => x.name == particle);
  }

}

static class ParticleNames {
  public static string FillTheseOnesForEasierUsageOfParticles = "";
}