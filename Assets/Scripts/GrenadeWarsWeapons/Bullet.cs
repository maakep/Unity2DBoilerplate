using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public int m_lifetime = 5;
	
	void Start () {
		Destroy(this.gameObject, m_lifetime);
	}
	
	void OnCollsionEnter(){
		Destroy(this.gameObject);
	}
}
