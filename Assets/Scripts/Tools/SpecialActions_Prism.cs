using UnityEngine;
using System.Collections;

public class SpecialActions_Prism : SpecialActions {

	public override void DoSpecialAction (string actionTag)
	{
		switch (actionTag) {
		case "ChangeToTeal":
			gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Visual/turquoise");
			break;
		default:
			if (gameObject.GetComponent<Interactable> ().Debugging) {
				Debug.Log (actionTag + " isn't defined in SpecialActions_Prism.");
			}
			break;
		}
	}
}
