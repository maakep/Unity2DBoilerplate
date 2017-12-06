using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenBar : MonoBehaviour {
	private float _penLevel = 1;
	/// <summary>
	/// 0-1, 1 is the full image, 0 is no bar
	/// </summary>
	public float penLevel
	{
		get
		{
				return _penLevel;
		}
		set
		{
				_penLevel = value;

				var scale = this.transform.localScale;
				scale.y = _penLevel;

				this.transform.localScale = scale;
		}
	}
}
