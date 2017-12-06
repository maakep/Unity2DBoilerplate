using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDocuments : MonoBehaviour {
	public List<GameObject> m_DocumentTrail = new List<GameObject>();
	public float distance = 2.5f;
	private Rigidbody2D rb;
	private SpriteRenderer sr;
	void Start() {
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		m_DocumentTrail.Add(gameObject);
		Physics2D.IgnoreLayerCollision(Layers.Player, Layers.Documents, true);
	}

	public void ClearDocuments() {
		m_DocumentTrail.RemoveAll(x => x.tag != Tags.Player);
	}

	public void RemoveDocument(GameObject document) {
		var index = m_DocumentTrail.IndexOf(document);
		var count = m_DocumentTrail.Count - index;
		if (index > -1) {
			m_DocumentTrail.RemoveRange(index, count);
		}
	}
	public void AddDocument(Document doc)
	{
		var lastDocument = m_DocumentTrail[m_DocumentTrail.Count-1];

		var pos = transform.position;
		
		// Check which way sprite is facing, deciding if teleporting to left or right side of sprite
		if (!sr.flipX) {
			pos.x += m_DocumentTrail.Count * distance;
		} else {
			pos.x -= distance;
		}
		
		doc.transform.position = pos;
		doc.GetComponent<DistanceJoint2D>().connectedBody = lastDocument.GetComponent<Rigidbody2D>();
		doc.Parent = lastDocument;

		m_DocumentTrail.Add(doc.gameObject);
	}

	public void DropDocument(GameObject docObj) {
		Debug.Log("Trying to drop " + docObj.name);
		var doc = docObj.GetComponent<Document>();
		if (doc != null) {
			doc.Parent = null;
			doc.GetComponent<DistanceJoint2D>().connectedBody = null;
			RemoveDocument(docObj);
			doc.tag = Tags.Grabbable;
		}
	}

	void Update() {
		if (Input.GetKeyDown(KeyMappings.DropDocument)) {
			DropDocument(m_DocumentTrail[m_DocumentTrail.Count-1]);
		}
	}
}
