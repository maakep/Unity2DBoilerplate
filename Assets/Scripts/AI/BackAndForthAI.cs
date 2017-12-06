using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForthAI : MonoBehaviour {

	public float speed = 3;
	public float direction;

	public int turn_right = -10;
	public int turn_left = 10;
	Rigidbody2D rbody;
	SpriteRenderer sprite;
	// Use this for initialization
	void Start () {
		rbody = this.GetComponent<Rigidbody2D>();
		sprite = this.GetComponent<SpriteRenderer>();
		direction = -1;
	}
	

	void Update () {
		if (this.transform.position.x < turn_right ) {
			direction = 1;
			sprite.flipX = true;
		} else if (this.transform.position.x > turn_left){
			direction = -1;
			sprite.flipX = false;
		}
	}

	void FixedUpdate() {
		rbody.velocity = new Vector2(speed * direction, 0);
	}


}
