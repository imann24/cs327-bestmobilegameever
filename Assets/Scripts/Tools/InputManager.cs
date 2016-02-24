using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class InputManager : MonoBehaviour, IPointerClickHandler{
	#region IPointerClickHandler implementation
	public void OnPointerClick (PointerEventData eventData)
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit)){
			GameObject touched = hit.transform.gameObject;
			Vector3 touchedPos = touched.transform.position;
			Vector3 playerPos = GameManager.Instance.playerCharacter.transform.position;
			if(touched.tag == "Ground"){
				GameManager.Instance.playerCharacter.GetComponent<Movement> ().MoveTo (hit.point);
			}
		}
	}
	#endregion

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		if (UIManager.Instance.IsInteractionShowing) {
			return;
		}
		/**
		if(Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)){
				GameObject touched = hit.transform.gameObject;
				Vector3 touchedPos = touched.transform.position;
				Vector3 playerPos = GameManager.Instance.playerCharacter.transform.position;
				UnityEventQueueSystem.
				if(touched.tag == "Ground"){
					GameManager.Instance.playerCharacter.GetComponent<Movement> ().MoveTo (hit.point);
				}
			}
		}**/
	
	}

}
