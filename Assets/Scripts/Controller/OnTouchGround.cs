using UnityEngine;
using System.Collections;

public class OnTouchGround : MonoBehaviour {

	void OnMouseDown(){
		
	}
	void OnMouseUp(){
		PlayerMovement.Instance.MoveTowards(new Vector2 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y) , false);
	}
	void OnMouseStay(){
		
	}


}
