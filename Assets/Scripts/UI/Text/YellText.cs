using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YellText : APopupText {

	protected override void ExtendedStart()
	{
		AnimatorClipInfo[] clip = m_popup_animator_text.GetCurrentAnimatorClipInfo(0);
		Destroy(this.gameObject, clip[0].clip.length);
	}
}
