using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class DebugScript : MonoBehaviour {
  void Update() {
    if (Input.GetKeyDown(KeyCode.Q)) {
      FadeManager.FadeIn(this, () => {
        Debug.Log("Done fading in");
      });
    }
    if (Input.GetKeyDown(KeyCode.Z)) {
      FadeManager.FadeOut(this, () => {
        Debug.Log("Done fading out");
      }, 0.1f);
    }
    if (Input.GetKeyDown(KeyCode.E)) {
      var p = ParticleManager.GetParticle("BloodExplosion");
      var pInstance = Instantiate(p);
      pInstance.transform.position = this.transform.position;
      _(5, () => { Destroy(pInstance); });
      
    }
    if (Input.GetKeyDown(KeyCode.R)) {
      SoundManager.Play(this, "bic", this.transform.position);
    }
    if (Input.GetKeyDown(KeyCode.F)) {
      
    }
    if (Input.GetKeyDown(KeyCode.G)) {
      
    }
    if (Input.GetKeyDown(KeyCode.C)) {
      
    }

  }


  void _(float sec, Action cb) {
    StartCoroutine(SimulateTime(sec, cb));
  }
  IEnumerator SimulateTime(float sec, Action cb) {
    yield return new WaitForSeconds(sec);
    cb();
  }
}