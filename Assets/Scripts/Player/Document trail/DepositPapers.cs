using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DepositPapers : MonoBehaviour {
	public DocumentTypes acceptedDocuments = DocumentTypes.Red;
	private SpriteRenderer spriteRenderer;

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == Tags.Player) {
			var pu = col.GetComponent<PickUpDocuments>();
			List<GameObject> objectsToRemove = new List<GameObject>();
			foreach(var doc in pu.m_DocumentTrail.Where(x => x.tag != Tags.Player && x.GetComponent<Document>().DocumentType == acceptedDocuments)) {
				PlayerStats.DocumentsDepositedCount++;
				objectsToRemove.Add(doc);
				Destroy(doc);
				
				if (PlayerStats.DocumentsDepositedCount >= PlayerStats.DocumentsWinCriterea) {
					Debug.Log("player win");
					FadeManager.FadeOut(this, () => {
						SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
					});
				}
			}

			foreach (var doc in objectsToRemove)
				pu.RemoveDocument(doc);

			if(objectsToRemove.Count() > 0) {
				SoundManager.Play(this, "paper_deposit", transform.position);
			}
		}
	}
}
