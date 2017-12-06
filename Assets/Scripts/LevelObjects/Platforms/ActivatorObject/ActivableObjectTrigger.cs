using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableObjectTrigger : MonoBehaviour, IInteractable {

	[Tooltip("An object with a script that implements IActivable")]
	public GameObject ActivableObject;
	private IActivatable _activableObject;
	private SpriteRenderer _sr;
	void Start() {
		if (ActivableObject == null) {
			Debug.LogError(name + " has no Activable Object set.");
		}
		_activableObject = ActivableObject.GetComponent<IActivatable>();
		if (_activableObject == null) {
			Debug.LogError("The object set in " + name + " doesn't implement IActivatable");
		}

		_sr = GetComponent<SpriteRenderer>();
	}

	public void Interact(GameObject sender) {
		if (_activableObject != null) {
			_sr.color = (_activableObject.Activate()) ? Color.green : Color.red;
		}
	}
}
