using UnityEngine;
using System.Collections;

public class SpecialActions_Emerald : SpecialActions {
	public GameObject Sapphire;
	public GameObject Topaz;
	
	public override void DoSpecialAction (string actionTag)
	{
		bool destroy = false;
		switch (actionTag) {
		case "ChangeToSapphire":
			destroy = true;
			Instantiate (Sapphire,transform.position,Quaternion.identity);
			break;
		case "SpawnTopaz":
			Instantiate (Topaz, transform.position, Quaternion.identity);
			break;
		default:
			if (gameObject.GetComponent<Interactable> ().Debugging) {
				Debug.Log (actionTag + " isn't defined in SpecialActions_Prism.");
			}
			break;
		}
		if (destroy) {
			Destroy (gameObject);
		}
	}
}
