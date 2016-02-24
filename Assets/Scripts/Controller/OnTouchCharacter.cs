using UnityEngine;
using System.Collections;

public class OnTouchCharacter : MonoBehaviour {
	private Renderer rend;
	bool _debug = false;
	bool _mouseDown;

	public InteractionID InteractionType;

	void Start()
	{
		rend = GetComponent<Renderer> ();
	}

	void OnMouseDown(){
		if (_debug) {
			Debug.Log ("Tapped");
		}

		EventController.Event(InteractionType);

		_mouseDown = true;
	}
	void OnMouseUp(){
		PlayerMovement.Instance.MoveTowards (transform.position, true);

		if (_debug) {
			Debug.Log ("Let go");
		}

		_mouseDown = false;
	}
	void OnMouseOver(){
		if (_mouseDown) {

			if (_debug) {
				Debug.Log ("Holding");
			}
		}
	}
	
}