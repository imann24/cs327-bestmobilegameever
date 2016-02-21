using UnityEngine;
using System.Collections;

public class OnTouchGround : MonoBehaviour {
	private Renderer rend;

	public PlayerMovement player;
	void Start()
	{
		rend = GetComponent<Renderer> ();
	}

	void OnTouchDown(){
		Debug.Log ("Ground tap");
	}
	void OnTouchUp(){
		player.MoveTowards (transform.position, false);
		Debug.Log ("Let go");
	}
	void OnTouchStay(){
		Debug.Log ("Holding");
	}

}
