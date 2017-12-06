using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats {
	public static int PenCount = 0;
	public static int GoldCount = 0;
	public static int DocumentsDepositedCount = 0;
	public static int DocumentsWinCriterea = int.MaxValue;

	public static void Initialize() {
		PenCount = 0;
		GoldCount = 0;
		DocumentsDepositedCount = 0;
		DocumentsWinCriterea = GameObject.FindObjectsOfType(typeof(Document)).Length;
	}
}
