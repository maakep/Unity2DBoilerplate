using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManController : MonoBehaviour {
	public float Speed;
	private int _direction = 1;
	private Vector3 _movement;

	private Rigidbody2D _rb;
	private SpriteRenderer _sr;

	public GameObject Document;

	// Use this for initialization
	void Start () {
		_rb = GetComponent<Rigidbody2D>();
		_sr = GetComponent<SpriteRenderer>();

		_movement = Vector3.right * Speed;
	}
	
	// Update is called once per frame
	void Update () {
		_rb.velocity = _movement;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.tag == "ManPoint") {
			_movement *= -1;
			_sr.flipX = !_sr.flipX;
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if(collision.transform.tag == "Player") {
			GameObject go = (GameObject)Instantiate(Document, transform.position, transform.rotation);
			PlayerStats.DocumentsWinCriterea++;
			SoundManager.Play(this, "manbump", transform.position);
		}
	}
}
