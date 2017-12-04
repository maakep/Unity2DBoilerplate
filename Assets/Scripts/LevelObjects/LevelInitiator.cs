using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitiator : MonoBehaviour {
	void Awake () {
		PlayerStats.Initialize();
	}
}
