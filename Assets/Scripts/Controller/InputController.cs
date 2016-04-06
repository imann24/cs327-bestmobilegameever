using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {
	private Camera mainCamera;
	private RayPathFinding path;
	private OrangeThrowing oranges;
		
	bool _debug = false;
	void Start(){
		path = PlayerMovement.Instance.transform.GetComponent<RayPathFinding> ();
		oranges = PlayerMovement.Instance.transform.GetComponent<OrangeThrowing> ();
		mainCamera = this.gameObject.GetComponent<Camera> (); 
	}

	// Update is called once per frame
	void Update () {
        // Does not check for player input if the dialogue is currently active
        if (!GameManager.UIManager.CanInteract) { return; }

		#if UNITY_EDITOR
		if (Input.GetMouseButtonUp(0)) {
			Ray ray = mainCamera.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(new Vector2 (ray.origin.x, ray.origin.y), ray.direction);

			if (hit) {
				GameObject recipient = hit.transform.gameObject;

				if (recipient.tag == "Clickable") {
					if (Input.GetMouseButtonDown(0)) {
						recipient.SendMessage ("OnTouchDown", SendMessageOptions.DontRequireReceiver);
					}
					if (Input.GetMouseButtonUp(0)) {
						recipient.SendMessage ("OnTouchUp", SendMessageOptions.DontRequireReceiver);
					}
					if (Input.GetMouseButton(0)) {
						recipient.SendMessage ("OnTouchStay", SendMessageOptions.DontRequireReceiver);
					}
				}
				else if (recipient.tag == "Ground"){
					if (_debug) {
						Debug.Log (mainCamera.ScreenToWorldPoint(Input.mousePosition).x);
					}
					path.GetPath(new Vector2 (hit.point.x, hit.point.y));
				}
			}
		}
		#endif
		if (Input.touchCount > 0) {

			foreach (Touch touch in Input.touches) {
				Ray ray = mainCamera.ScreenPointToRay (touch.position);
				RaycastHit2D hit = Physics2D.Raycast(new Vector2 (ray.origin.x, ray.origin.y), ray.direction);

				if (oranges.hasOrange) {
					if (touch.phase == TouchPhase.Ended) {
						oranges.toThrow = true;
					}
				}

				if (hit) {
					GameObject recipient = hit.transform.gameObject;

					if (recipient.tag == "Clickable") {
						if (touch.phase == TouchPhase.Began) {
							recipient.SendMessage ("OnTouchDown", SendMessageOptions.DontRequireReceiver);
						}
						if (touch.phase == TouchPhase.Ended) {
							recipient.SendMessage ("OnTouchUp", SendMessageOptions.DontRequireReceiver);
						}
						if (touch.phase == TouchPhase.Stationary) {
							recipient.SendMessage ("OnTouchStay", SendMessageOptions.DontRequireReceiver);
						}
						if (touch.phase == TouchPhase.Canceled) {
							recipient.SendMessage ("OnTouchExit", SendMessageOptions.DontRequireReceiver);
						}
					} else if (recipient.tag == "Ground") {
						if (_debug) {
							Debug.Log (mainCamera.ScreenToWorldPoint (touch.position).x);
						}
						if (touch.phase == TouchPhase.Began) {
							if (oranges.hasOrange) {
								return;
							}
							path.GetPath (new Vector2 (hit.point.x, hit.point.y));
						}
					} else if (recipient.tag == "Player") {
						if (touch.phase == TouchPhase.Stationary) {
							hit.transform.GetComponent<OrangeThrowing> ().Activate ();
						}
					}
				} 
			}
		}
	}
}
