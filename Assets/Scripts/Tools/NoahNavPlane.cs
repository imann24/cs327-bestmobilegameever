using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class NoahNavPlane : MonoBehaviour, IPointerClickHandler {

	NavMeshAgent Player;

	public bool flipped = false;
	private SpeechBubble speechBubble;

	// Use this for initialization
	void Start () {
		Player = GameManager.PlayerCharacter.GetComponent<NavMeshAgent>();
		speechBubble = Player.GetComponentInChildren<SpeechBubble>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp(){
		//Debug.Log ("Mouse UP");
	}

	public void Flip(){ //Flip player character
		flipped = !flipped;
		Player = GameManager.PlayerCharacter.GetComponent<NavMeshAgent>();
		speechBubble = Player.GetComponentInChildren<SpeechBubble>();
	
		Player.transform.localScale = new Vector3 (Player.transform.localScale.x * -1, Player.transform.localScale.y, Player.transform.localScale.z);
		speechBubble.transform.localScale = new Vector3(speechBubble.transform.localScale.x * -1, speechBubble.transform.localScale.y, speechBubble.transform.localScale.z);
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		if (!UIManager._instance.paused){
			#if DEBUG
			Debug.Log("Clicked on Nav Floor.");
			#endif
			NavMeshPath path = new NavMeshPath ();
			Vector3 destination = eventData.pointerCurrentRaycast.worldPosition;
			Player.GetComponent<NavMeshAgent> ().CalculatePath (destination, path);
			//if(path.status == NavMeshPathStatus.PathComplete){
			if (destination.x > Player.transform.position.x) {
				if (flipped) {
					Flip ();
				}
			} else {
				if (!flipped) {
					Flip ();
				}
			}

			Player.SetDestination (destination);

		}
	}
}
