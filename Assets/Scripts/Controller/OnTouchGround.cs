using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class OnTouchGround : MonoBehaviour, IPointerClickHandler {

	void OnMouseDown(){
		
	}
	void OnMouseUp(){
	//	PlayerMovement.Instance.MoveTowards(new Vector2 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y) , false);
	}

	#region IPointerClickHandler implementation
	public void OnPointerClick (PointerEventData eventData){
		Debug.Log ("Click!");
	}
	#endregion


	void OnMouseStay(){
		
	}


}
