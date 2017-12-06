using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenPickup : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == Tags.Player) {
			PlayerStats.PenCount++;
			SoundManager.Play(this, "pen_pickup", transform.position, 0.6f);
			Destroy(gameObject);
		}
	}
}
