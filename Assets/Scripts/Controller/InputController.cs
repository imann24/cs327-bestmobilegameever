using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {
	private Camera mainCamera;
	bool _debug = false;
	void Start(){
		mainCamera = this.gameObject.GetComponent<Camera> (); 
	}

	// Update is called once per frame
	void Update () {
		// Does not check for player input if the dialogue is currently active
		if (WorldController.Instance.DialogueActive) {
			return;
		}

		#if UNITY_EDITOR
		if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)) {

			Ray ray = mainCamera.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit)) {
				GameObject recipient = hit.transform.gameObject;

				if (recipient.tag == "Clickable") {
					if (Input.GetMouseButtonDown(0)) {
						recipient.SendMessage ("OnMouseDown", SendMessageOptions.DontRequireReceiver);
					}
					if (Input.GetMouseButtonUp(0)) {
						recipient.SendMessage ("OnMouseUp", SendMessageOptions.DontRequireReceiver);
					}
					if (Input.GetMouseButton(0)) {
						recipient.SendMessage ("OnMouseOver", SendMessageOptions.DontRequireReceiver);
					}
				}
				else if (recipient.tag == "Ground"){
					if (_debug) {
						Debug.Log (mainCamera.ScreenToWorldPoint(Input.mousePosition).x);
					}
					PlayerMovement.Instance.MoveTowards(new Vector2 (mainCamera.ScreenToWorldPoint(Input.mousePosition).x, mainCamera.ScreenToWorldPoint(Input.mousePosition).y) , false);
				}
			}
		}
		#endif
		if (Input.touchCount > 0) {

			foreach (Touch touch in Input.touches) {
				Ray ray = mainCamera.ScreenPointToRay (touch.position);
				RaycastHit hit;

				if (Physics.Raycast (ray, out hit)) {
					GameObject recipient = hit.transform.gameObject;

					if (recipient.tag == "Clickable") {
						if (touch.phase == TouchPhase.Began) {
							recipient.SendMessage ("OnMouseDown", SendMessageOptions.DontRequireReceiver);
						}
						if (touch.phase == TouchPhase.Ended) {
							recipient.SendMessage ("OnMouseUp", SendMessageOptions.DontRequireReceiver);
						}
						if (touch.phase == TouchPhase.Stationary) {
							recipient.SendMessage ("OnMouseOver", SendMessageOptions.DontRequireReceiver);
						}
						if (touch.phase == TouchPhase.Canceled) {
							recipient.SendMessage ("OnTouchExit", SendMessageOptions.DontRequireReceiver);
						}
					} 
					else if (recipient.tag == "Ground"){
						if (_debug) {
							Debug.Log (mainCamera.ScreenToWorldPoint(Input.mousePosition).x);
						}
						PlayerMovement.Instance.MoveTowards(new Vector2 (mainCamera.ScreenToWorldPoint(Input.mousePosition).x, mainCamera.ScreenToWorldPoint(Input.mousePosition).y) , false);
					}
				} 
			}
		}
	}
}
