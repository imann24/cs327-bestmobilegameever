using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpecialActions : MonoBehaviour {

	public void DoSpecialActions(List<string> actionList){
		foreach (string action in actionList) {
			DoSpecialAction (action);
		}
	}

	//override this function in subclasses for specific actions.
	public void DoSpecialAction(string actionTag){
	}
}
