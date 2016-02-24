using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class OrangeThrower : MonoBehaviour, IPointerDownHandler {

	public void OnPointerDown (PointerEventData eventData)
	{
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
