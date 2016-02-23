using UnityEngine;
using System.Collections;

public class MopSpecials : SpecialActions {

	public override void DoSpecialAction (string actionTag)
	{
		switch (actionTag) {
		case "Confirm":
			Debug.Log ("It works!");
			break;
		default:
			break;
		}
	}
}
