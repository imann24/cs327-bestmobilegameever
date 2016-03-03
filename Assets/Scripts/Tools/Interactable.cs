using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Interactable : MonoBehaviour, IPointerClickHandler {

	public List<Interaction> Interactions { get; private set; }
	public string InteractionPath;
	public bool Debugging;

	void Awake(){
		Interactions = InteractionList.Load ("Text/" + InteractionPath);
		if (GetComponent<SpecialActions> () == null) {
			gameObject.AddComponent<SpecialActions> ();
			if (Debugging) {
				Debug.Log ("There is no assigned SpecialActions behavior for " + gameObject.name);
			}
		}
		if (GetComponentInChildren<Canvas> () == null && Debugging) {
			Debug.Log ("There is no SpeechBubble assigned for " + gameObject.name + ". Check Assets/Prefabs if you need one.");
		}
	}
	// Use this for initialization
	void Start () {
		if (Debugging) {
			Debug.Log ("Interactions for " + gameObject.name + ": " + Interactions.Count);
			Debug.Log (string.Join (" ", Interactions.Select (i => i.iName).ToArray()));
		}
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		InteractionManager.HandleClick (this);
	}

	public void DoSpecialActions(List<string> actionsToDo){
		GetComponent<SpecialActions> ().DoSpecialActions (actionsToDo);
	}
}
