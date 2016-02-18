using UnityEngine;
using System.Collections;

public class OnTouchCharacter : MonoBehaviour {
	private Renderer rend;

	public PlayerMovement player;
	void Start()
	{
		rend = GetComponent<Renderer> ();
	}

	void OnTouchDown(){
		Debug.Log ("Tapped");
	}
	void OnTouchUp(){
		player.MoveTowards (transform.position, true);
		Debug.Log ("Let go");
	}
	void OnTouchStay(){
		Debug.Log ("Holding");
	}
	
}
