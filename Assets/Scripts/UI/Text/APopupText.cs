using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class APopupText : MonoBehaviour {
	
	protected Text m_text;
	public Animator m_popup_animation;


	void Start()
	{
		m_text = m_popup_animation.GetComponent<Text>();
		ExtendedStart();
	}

	protected abstract void ExtendedStart();

	public void SetText(string text){
		m_popup_animation.GetComponent<Text>().text = text;	
	}

}
