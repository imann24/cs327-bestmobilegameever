using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpecialActions : MonoBehaviour {

	public void DoSpecialActions(List<string> actionList){
		bool destroy = false;
		foreach (string action in actionList) {
			switch (action) {
			case "ReturnSelected":
				InventoryManager.Instance.ReturnSelected ();
				break;
			case "Destroy":
				destroy = true;
				break;
			case "ComeHere":
				GameManager.Instance.playerCharacter.GetComponent<Movement> ().MoveTo (transform.position);
				break;
			default:
				DoSpecialAction (action);
				break;
			}
		}
		if (destroy) {
			Destroy (gameObject);
		}
	}

	//override this function in subclasses for specific actions.
	public virtual void DoSpecialAction(string actionTag){
	}
}
