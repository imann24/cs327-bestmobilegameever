using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Interactable : MonoBehaviour {


	public List<Interaction> Interactions { get; private set; }
	public string InteractionsXmlPath;
	public Sprite InventoryImage;
	public string InventoryTag;
	public string HoldingTag;
	public float DistanceBarrier;
	public bool PickUp;

	// Use this for initialization
	void Awake(){
		Interactions = InteractionList.Load ("Text/" + InteractionsXmlPath);
	}

	void Start(){
		//*
		if (GameManager.DEBUGGING) {
			Debug.Log (Interactions.Count);
			foreach (Interaction i in Interactions) {
				Debug.Log (i.ToString ());
			}
		}//*/
	}

	public static GameObject InstantiateAsInventory(string path){
		GameObject go = Instantiate (Resources.Load<GameObject> ("Prefabs/" + path)) as GameObject;
		go.GetComponent<Collider> ().enabled = false;
		go.GetComponent<Renderer> ().enabled = false;
		return go;
	}

	void OnMouseUpAsButton(){
		List<Interaction> subList;
		if (InventoryManager.Instance.IsHoldingItem) {
			UseSelected ();
		} else {
			subList = Interactions.FindAll (interaction => interaction.iType == InteractionType.Click);
			if (GameManager.DEBUGGING) {
				Debug.Log ("Click Interaction. Number of potential interactions:" + subList.Count);
			}
			InteractionManager.HandleInteractionList (this,subList);
		}
	}

	public void UseSelected(){
		Interactable selected = InventoryManager.Instance.selectedItem.GetComponent<Interactable> ();
		List<Interaction> validItemsForTarget = Interactions.FindAll (i => (i.iType == InteractionType.UseItem) && (i.IsValid) && (i.iAllTags.Contains (selected.HoldingTag)));
		if (validItemsForTarget.Count > 0) {
			InteractionManager.HandleInteractionList (this, validItemsForTarget);
		} else {
			Interaction error = selected.Interactions.Find (i => i.iName == "DefaultCan'tUse");
			if (error != null) {
				InteractionManager.HandleInteractionSingle (this, error);
			} else {
				InventoryManager.Instance.ReturnSelected ();
			}
		}
	}

	public void OnOrange(){
		List<Interaction> subList = Interactions.FindAll (interaction => interaction.iType == InteractionType.Orange);
		InteractionManager.HandleInteractionList (this, subList);
	}

	public void DoSpecialActions(List<string> actionsToDo){
		gameObject.GetComponent<SpecialActions> ().DoSpecialActions (actionsToDo);
	}

	public void Dematerialize(){
		gameObject.GetComponent<Renderer> ().enabled = false;
		gameObject.GetComponent<Collider> ().enabled = false;
	}

	public void Rematerialize(){
		gameObject.GetComponent<Renderer> ().enabled = true;
		gameObject.GetComponent<Collider> ().enabled = true;
	}

	public void Despawn(){
		Destroy (gameObject);
	}
}
