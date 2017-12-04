using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SubmitDocument : MonoBehaviour {

	private bool _conditionsMet = false;
	public GameObject PaintMan;
	void Start () {
		PlayerStats.DocumentsDepositedCount--;

		GetComponent<Button>().onClick.AddListener(() => {
			_conditionsMet = PaintManager.CheckAllFields();
			if (_conditionsMet) {
				if (PlayerStats.DocumentsDepositedCount > 0) {
					PlayerStats.DocumentsDepositedCount--;
					PaintManager.ClearAllFields();
					GameObject.Find("Contract").GetComponent<ContractInitiator>().GenerateNew();
				} else {
					FadeManager.FadeOut(this, () => {
						SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
					});
				}
			} else {
				SoundManager.Play(this, "error", Vector3.zero, 0.2f);
				StartCoroutine(WarnAWhile());
			}
		});
	}

	IEnumerator WarnAWhile() {
		transform.GetChild(0).GetComponent<Text>().text = "Please properly fill all fields";
		yield return new WaitForSeconds(2f);
		transform.GetChild(0).GetComponent<Text>().text = "SUBMIT";
	}
}
