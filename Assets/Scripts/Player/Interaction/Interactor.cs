using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour {
  public float interactRangeRadius;
  public bool multipleInteractionsPerClick = false;
  private int _layerMask;

  void Awake() {
    _layerMask = ~(1 << Layers.Player);
  }

  void Update() {
    if (Input.GetButtonDown(KeyMappings.Interact)) {
      foreach (var col in Physics2D.OverlapCircleAll(transform.position, interactRangeRadius, _layerMask)) {
        var interactables = col.GetComponents<IInteractable>();
        foreach (var interactable in interactables) {
          interactable.Interact(gameObject);
          if (!multipleInteractionsPerClick && interactables != null) {
            return;
          }
        }        
      }
    }
  }
}