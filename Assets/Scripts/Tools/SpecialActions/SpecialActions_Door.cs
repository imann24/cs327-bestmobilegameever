using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SpecialActions_Door : SpecialActions {

	public override void DoSpecialAction(string actionTag) {
		switch (actionTag) {
		case "SoundOpenDoor":
			EventController.Event("DoorOpen");
			break;
		default:
			if (gameObject.GetComponent<Interactable>().Debugging) { Debug.Log(actionTag + " isn't defined in SpecialActions_Door."); }
			break;
		}
	}
}
