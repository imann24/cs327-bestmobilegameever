using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class NoahNavPlane : MonoBehaviour, IPointerClickHandler {

	NavMeshAgent Player;

	public bool flipped = false;
	private SpeechBubble speechBubble;
	private NoahMove playerMove;
	public bool moving = false;
	private Vector3 destination;
	// Use this for initialization
	void Start () {
		Player = GameManager.PlayerCharacter.GetComponent<NavMeshAgent>();
		playerMove = Player.GetComponent<NoahMove> ();
		speechBubble = Player.GetComponentInChildren<SpeechBubble>();

	}
	
	// Update is called once per frame
	void Update () {
		if (moving) {
			if (Player.velocity.x > 0) {
				if (flipped) {
					Flip ();
				}
			} else if (Player.velocity.x < 0) {
				if (!flipped) {
					Flip ();
				}
			}
		}
		moving = Player.hasPath;
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
			destination = eventData.pointerCurrentRaycast.worldPosition;
			Player.GetComponent<NavMeshAgent> ().CalculatePath (destination, path);
			//if(path.status == NavMeshPathStatus.PathComplete){
			GameManager.InventoryManager.Hide();

			Player.SetDestination (destination);

		}
	}
}
