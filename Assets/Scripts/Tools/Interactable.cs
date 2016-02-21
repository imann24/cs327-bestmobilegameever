using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Interactable : MonoBehaviour {


	public List<Interaction> Interactions { get; private set; }
	public string InteractionsXmlPath;
	public Sprite InventoryImage;
	public string InventoryTag;
	public string HoldingTag;
	public bool PickUp;

	// Use this for initialization
	void Awake(){
		Interactions = InteractionList.Load ("Text/" + InteractionsXmlPath);
	}

	void Start(){
		if (GameManager.DEBUGGING) {
			Debug.Log (Interactions.Count);
			foreach (Interaction i in Interactions) {
				Debug.Log (i.ToString ());
			}
		}
	}

	void OnMouseUpAsButton(){
		if (GameManager.DEBUGGING) {
			Debug.Log ("Clicked!");
		}
		List<Interaction> subList;
		if (InventoryManager.Instance.IsHoldingItem) {
			subList = Interactions.FindAll (interaction => interaction.iType == InteractionType.UseItem);
			if (GameManager.DEBUGGING) {
				Debug.Log ("Use Interaction. Number of potential interactions:" + subList.Count);
			}
		} else {
			subList = Interactions.FindAll (interaction => interaction.iType == InteractionType.Click);
			if (GameManager.DEBUGGING) {
				Debug.Log ("Click Interaction. Number of potential interactions:" + subList.Count);
			}
		}
		InteractionManager.HandleInteractions (this,subList);
	}

	public void OnOrange(){
		List<Interaction> subList = Interactions.FindAll (interaction => interaction.iType == InteractionType.Orange);
		InteractionManager.HandleInteractions (this, subList);
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
