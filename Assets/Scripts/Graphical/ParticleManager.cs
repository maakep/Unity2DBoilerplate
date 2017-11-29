using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ParticleManager
{
  private List<ParticleSystem> _particles = new List<ParticleSystem>();
  public ParticleManager() {
    _particles = Resources.LoadAll<ParticleSystem>("/Particles").ToList();
  }

  private ParticleSystem GetParticle(string particle) {
    return _particles.FirstOrDefault(x => x.name == particle) ?? _particles[0];
  }

}

static class ParticleNames {
  public static string FillTheseOnesForEasierUsageOfParticles = "";
}