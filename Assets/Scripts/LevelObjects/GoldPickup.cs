using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickup : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == Tags.Player) {
			PlayerStats.GoldCount++;
			SoundManager.Play(this, "gold_pickup", transform.position, 0.6f);
			Destroy(gameObject);
		}
	}
}
