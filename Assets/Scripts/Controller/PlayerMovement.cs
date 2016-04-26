using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public float moveSpeed = 1f;

	public float camMoveSpeed;
	public float camMoveOffset = 1f;
	public bool moving = false;

	private bool flipped = false;

	public static PlayerMovement Instance
	{
		get
		{
			return instance;
		}
	}
	private static PlayerMovement instance = null;

	void Awake (){
		if(instance){
			DestroyImmediate(gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad(gameObject);
	}

	void Flip(){ //Flip player character
		flipped = !flipped;
        SpeechBubble speechBubble = gameObject.GetComponentInChildren<SpeechBubble>();
		transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
		speechBubble.transform.localScale = new Vector3(Mathf.Abs(speechBubble.transform.localScale.x), speechBubble.transform.localScale.y, speechBubble.transform.localScale.z);
    }

	public void MoveTowards(Vector2 location, bool person){
		if (!moving) {
			if (location.x > transform.position.x) {
				if (flipped) {
					Flip ();
				}
			} else {
				if (!flipped) {
					Flip ();
				}
			}
			if (person) {
				if (location.x > transform.position.x) {
					location = new Vector2 (location.x - 1f, location.y);
				} else {
					location = new Vector2 (location.x + 1f, location.y);
				}
			}
			StartCoroutine ("MovePlayerAndLerpCam", location);
		}
	}


	IEnumerator MovePlayerAndLerpCam(Vector2 endLocation){
		moving = true;
		float percentComplete = 0f;
		// Cam Lerp
		bool moveCam = false;
		Vector3 startPos = Camera.main.transform.position;
		Vector3 endCamPos = new Vector3 (endLocation.x, endLocation.y, Camera.main.transform.position.z);
		while (Vector2.Distance(transform.position, endLocation) > 0.1f) {
			//Move Camera
			if (percentComplete <= 1.0f) {
				if (Vector2.Distance (Camera.main.transform.position, PlayerMovement.Instance.transform.position) >= camMoveOffset) {
					moveCam = true;
				}
			} else {
				moveCam = false;
			}

			if (moveCam) {
				percentComplete += Time.deltaTime * camMoveSpeed;
				Camera.main.transform.position = Vector3.Lerp (startPos, endCamPos, percentComplete);
			}
			//Move Player

			transform.position = Vector2.MoveTowards (transform.position, endLocation, moveSpeed * Time.deltaTime);

			yield return new WaitForFixedUpdate();
		}
		moving = false;
	}
}
