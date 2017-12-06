using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	GameObject player;
	Vector3 offset;
	void Start () {
		player = GameObject.FindWithTag("Player");
		offset = player.transform.position - this.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		this.transform.position = player.transform.position - offset;
	}
}
