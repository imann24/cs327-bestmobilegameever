using UnityEngine;
using System.Collections;

public class OrangeThrowing : MonoBehaviour {

	public GameObject orangePrefab;
	public float minX;
	public float maxX;
	public float minY;
	public float maxY;

	private bool hasOrange = false;
	private GameObject heldOrange;
	private bool throwing = false;

	private Touch heldTouch;
	// Update is called once per frame
	#if UNITY_EDITOR 
	void Update () {
		if (Input.GetKeyDown (KeyCode.F)) {
			if (!hasOrange) {
				if (!throwing) {
					GetNewOrange ();
				}
			}
		}
	}
	#endif
	public void Activate(Touch currentTouch){
		if (!hasOrange) {
			if (!throwing) {
				heldTouch = currentTouch;
				GetNewOrange ();
			}
		}
	}

	void GetNewOrange(){
		hasOrange = true;
		throwing = true;
		heldOrange = (GameObject) Instantiate (orangePrefab, new Vector2 (transform.position.x + (-1f * Mathf.Sign(transform.localScale.x)), transform.position.y + 0.5f), Quaternion.identity);
		heldOrange.transform.parent = transform;
		StartCoroutine ("ThrowingOrange");
	}

	IEnumerator ThrowingOrange(){
		//Change position of orange according to mouse position
		Vector2 orangeForce = Vector2.zero;
		Rigidbody2D orangeRb = heldOrange.GetComponent<Rigidbody2D> ();
		bool done = false;
		float dir = Mathf.Sign (transform.localScale.x);
		float xVel = 0f;
		float yVel = 0f;
		while (!done) {
			Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			dir = Mathf.Sign (transform.localScale.x);

			if (dir == 1f) {
				xVel = Mathf.Clamp ((transform.position.x - mousePos.x) * dir, minX, maxX);
				yVel = Mathf.Clamp ((mousePos.y - transform.position.y) * -5f, minY, maxY);
			} else {
				xVel = Mathf.Clamp ((transform.position.x - mousePos.x) * dir, -minX, -maxX);
				yVel = Mathf.Clamp ((mousePos.y - transform.position.y) * -5f, minY, maxY);
			}

			if (Input.GetKeyUp (KeyCode.F)) {
				done = true;
			}
			#if !UNITY_EDITOR
			if (heldTouch) {
				if (heldTouch.phase = TouchPhase.Ended) {
					done = true;
				}
			}
			#endif
			yield return null;
		}
		heldOrange.transform.parent = null;
		orangeForce = new Vector2(xVel, yVel);
		orangeRb.constraints = RigidbodyConstraints2D.None;
		orangeRb.velocity = orangeForce;
		hasOrange = false;
		throwing = false;
	}
}
