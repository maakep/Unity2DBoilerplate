using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillOnTouch : MonoBehaviour {
	public Color m_FadeColor = Color.black;
	public float FadeSpeed;

	private PickUpDocuments _trail;

	void Start() {
		_trail = GameObject.FindWithTag(Tags.Player).GetComponent<PickUpDocuments>();
	}
	void OnTriggerEnter2D(Collider2D col) {
		var doc = col.GetComponent<Document>();
		if (col.tag == Tags.Player) {
			FadeManager.FadeOut(this, () => {
				SceneManager.LoadScene("40_EndScreen_Burnout");
			}, m_FadeColor, FadeSpeed);
		}
		else if (doc != null) {
			var joint = doc.GetComponent<DistanceJoint2D>();
			joint.connectedBody = null;
			var obj = Instantiate(doc.gameObject, doc.SpawnPosition, Quaternion.identity);
			obj.name = "Document";
			_trail.RemoveDocument(doc.gameObject);
			Destroy(doc.gameObject);
		}
	}
}
