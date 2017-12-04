using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleScriptOnActivation : MonoBehaviour, IActivatable {

	public MonoBehaviour otherScript;
	public bool Activate() {
		return otherScript.enabled = !otherScript.enabled;
	}
}
