using UnityEngine;
using System.Collections;

public class OnTouchCharacter : MonoBehaviour {
	private Renderer rend;
	void Start()
	{
		rend = GetComponent<Renderer> ();
	}

	void OnTouchDown(){
		Debug.Log ("Tapped");
	}
	void OnTouchUp(){
		PlayerMovement.Instance.MoveTowards (transform.position, true);
		Debug.Log ("Let go");
	}
	void OnTouchStay(){
		Debug.Log ("Holding");
	}
	
}
