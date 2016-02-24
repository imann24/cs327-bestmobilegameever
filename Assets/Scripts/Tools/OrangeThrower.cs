using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class OrangeThrower : MonoBehaviour{//, IPointerDownHandler {

	public void OnMouseDown ()//(PointerEventData eventData)
	{
		if (GameManager.DEBUGGING) {
			Debug.Log ("Pointer Down on Player. Winding up!");
		}
		Orange.WindUp (gameObject);
	}

	/**
	void OnMouseDown(){
		WindUpOrange ();
	}

	void WindUpOrange(){
		Instantiate (Resources.Load<GameObject> ("Prefabs/Orange"), transform.position, Quaternion.identity);
	}**/
}
