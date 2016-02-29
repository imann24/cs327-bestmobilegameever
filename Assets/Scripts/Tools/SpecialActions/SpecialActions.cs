using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpecialActions : MonoBehaviour {

    private Dictionary<string, SpecialActions_Extended> actionScripts = new Dictionary<string, SpecialActions_Extended>();

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
                if (GetComponent<SpecialActions_Extended>() != null) { actionScripts[action].DoExtendedAction(); }
                DoSpecialAction (action);
				break;
			}
		}
		if (destroy) {
			Destroy (gameObject);
		}
	}

    public void Start() {
        SpecialActions_Extended[] scripts = GetComponents<SpecialActions_Extended>();
        foreach (SpecialActions_Extended script in scripts) { actionScripts.Add(script.ActionTag, script); }
    }

    //override this function in subclasses for specific actions.
    public virtual void DoSpecialAction(string actionTag) {
		if (gameObject.GetComponent<Interactable> ().Debugging) {
			Debug.Log (actionTag + " is not defined. Using default SpecialActions script.");
		}
	}
}