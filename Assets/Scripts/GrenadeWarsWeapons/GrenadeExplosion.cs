using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplosion : MonoBehaviour {

	public float m_increase_speed = 2f;
	public float m_lifetime = 4f;
	public float m_force_multiplier = 25f;
	private Vector2 m_position;
	// Use this for initialization
	void Start () {
		m_position = this.gameObject.transform.position; 
		Destroy(this.gameObject, m_lifetime);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.localScale += new Vector3(m_increase_speed, m_increase_speed, 0) * Time.deltaTime;
	}

	void OnTriggerStay2D(Collider2D coll) {
		Debug.Log("other");
		if (coll.gameObject.tag == Tags.Player) {
			Debug.Log("player");
			Vector3 p_player = coll.transform.position;
			float dir_x = p_player.x - m_position.x;
			float dir_y = p_player.y - m_position.y;

			// TODO PERFORM DAMAGE on player.


			coll.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir_x, dir_y).normalized * m_force_multiplier * 10);
		}

		if (coll.gameObject.tag == Tags.Ground) { 
			Debug.Log("grenade");
			Vector3 p_grenade = coll.transform.position;
			float dir_x = p_grenade.x - m_position.x;
			float dir_y = p_grenade.y - m_position.y;

			// TODO PERFORM DAMAGE on player.


			coll.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir_x, dir_y) * m_force_multiplier);
		}

	}
	
}
