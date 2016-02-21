using UnityEngine;
using System.Collections;

public class OnTouchGround : MonoBehaviour {
	private Renderer rend;
	void Start()
	{
		rend = GetComponent<Renderer> ();
	}

	void OnTouchUp(){
		PlayerMovement.Instance.MoveTowards (transform.position, false);
	}
}
