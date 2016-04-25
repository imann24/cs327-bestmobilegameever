using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Interactable : MonoBehaviour, IPointerClickHandler, IDragHandler {

	public List<Interaction> Interactions { get; private set; }
	public string InteractionPath;
	public bool Debugging;
	public bool flipped=false;
	public NavMeshAgent agent;
	private SpeechBubble speechBubble;
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

	#region IDragHandler implementation
	public void OnDrag (PointerEventData eventData)
	{
		//transform.position = Camera.main.ScreenToWorldPoint( Input.mousePosition);
	}
	#endregion

	public void DoSpecialActions(List<string> actionsToDo){
		GetComponent<SpecialActions> ().DoSpecialActions (actionsToDo);
	}

	public void Flip(){ //Flip player character
		if(gameObject.GetComponent<NavMeshAgent>()!=null && gameObject.tag!="DontFlip" ) 
		{
			agent = gameObject.GetComponent<NavMeshAgent>();

			flipped = !flipped;
			speechBubble = agent.GetComponentInChildren<SpeechBubble> ();
			agent.transform.localScale = new Vector3 (agent.transform.localScale.x * -1, agent.transform.localScale.y, agent.transform.localScale.z);
			speechBubble.transform.localScale = new Vector3 (speechBubble.transform.localScale.x * -1, speechBubble.transform.localScale.y, speechBubble.transform.localScale.z);

		}
	}

}
