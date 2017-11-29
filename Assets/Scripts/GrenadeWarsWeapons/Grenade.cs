using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {


	public float m_lifetime = 5;
	public GameObject explosion;
	
	void Start () {
		Destroy(this.gameObject, m_lifetime);
	}

	void OnDestroy(){
		GameObject instance = Instantiate(explosion);
		instance.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
	}
	
}