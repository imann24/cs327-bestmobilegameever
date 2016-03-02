using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SpecialActions : MonoBehaviour {
	public void DoSpecialActions(List<string> actionsToDo){
		bool destroy = false;
		foreach (string action in actionsToDo) {
			switch (action) {
			case "ReturnSelected":
				GameManager.InventoryManager.ReturnSelected ();
				break;
			case "ComeHere":
				break;
			case "Destroy":
				destroy = true;
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

	public virtual void DoSpecialAction(string actionToDo){
		if (GetComponent<Interactable> ().Debugging) {
			Debug.Log ("There is no defined action " + actionToDo + " for " + gameObject.name + ", it has the base SpecialActions behavior.");
		}
	}
}

