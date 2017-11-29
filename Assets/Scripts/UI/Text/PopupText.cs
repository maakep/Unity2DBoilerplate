using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupText : APopupText {

	protected override void ExtendedStart()
	{
		AnimatorClipInfo[] clip = m_popup_animation.GetCurrentAnimatorClipInfo(0);
		Destroy(this.gameObject, clip[0].clip.length);
		Debug.Log(clip[0].clip.length);
	}

}
