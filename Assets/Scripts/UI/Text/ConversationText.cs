using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationText : APopupText {
	
	public Animator m_popup_animator_full;
	private Transform m_follow_object;
	private Vector3 m_pad_y;
	private AnimatorClipInfo[] m_clip; 
	private AnimatorClipInfo[] m_clip_2;
	protected override void ExtendedStart()
	{
		m_clip = m_popup_animator_text.GetCurrentAnimatorClipInfo(0);
		m_clip_2 = m_popup_animator_text.GetCurrentAnimatorClipInfo(0);
	}

	public override void DestroyText()
	{
		Destroy(this.transform.parent.gameObject);
	}
}
