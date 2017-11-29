using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestText : MonoBehaviour {

	float m_time;

	void Start () {
		m_time = Time.deltaTime;
	}
	

	void Update () {
		if (m_time < 0.1){
			m_time += Time.deltaTime;
		} else {
			m_time = 0;
			TextManager.CreateDmgText(Random.Range(-10,10).ToString(), this.transform, 0.5f);
		}
		
	}
}
