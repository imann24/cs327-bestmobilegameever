using UnityEngine;
using System.Collections;

public class Orange : MonoBehaviour {

	bool shouldFollowMouse = true;
	bool notReleased = true;
	GameObject thrower;
	float maxWindup = 4;
	float lifeSpan = 20;
	float previousVelocity = 0;
	bool hitSomething = false;
	// Use this for initialization
	void Start () {
		thrower = GameObject.Find ("OrangeThrower");
		gameObject.GetComponent<SpringJoint> ().connectedBody = thrower.GetComponent<Rigidbody> ();
			
	}

	void FollowMouse(){
		Ray mouseCast = Camera.main.ScreenPointToRay (Input.mousePosition);
		Plane throwerParallel = new Plane (Camera.main.transform.position, thrower.transform.position);
		float alongCast;
		throwerParallel.Raycast (mouseCast, out alongCast);
		transform.position = Vector2.MoveTowards (thrower.transform.position, mouseCast.GetPoint (alongCast), maxWindup);
	}

	void Throw(){
		Ray mouseCast = Camera.main.ScreenPointToRay (Input.mousePosition);
		Plane throwerParallel = new Plane (Camera.main.transform.position, thrower.transform.position);
		RaycastHit hit;
		if (Physics.Raycast (mouseCast, out hit) && hit.collider.gameObject == thrower) {
			Destroy (gameObject);
		} else {
			gameObject.GetComponent<Rigidbody> ().isKinematic = false;
			shouldFollowMouse = false;
		}
	}

	void OnCollisionEnter(Collision collision){
		Interactable interactableHit = collision.gameObject.GetComponent<Interactable> ();
		if (interactableHit != null && hitSomething == false) {
			hitSomething = true;
			interactableHit.OnOrange ();
		}
	}

	void Release(){
		Destroy (gameObject.GetComponent<SpringJoint> ());
		notReleased = false;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.rotation = Quaternion.Euler (Vector3.zero);
		if (shouldFollowMouse) {
			FollowMouse ();
		} else {
			lifeSpan -= Time.deltaTime;
			if (lifeSpan <= 0) {
				Destroy (gameObject);
			}
			if (gameObject.GetComponent<Rigidbody> ().velocity.magnitude < previousVelocity && notReleased) {
				Release ();
			}
			previousVelocity = gameObject.GetComponent<Rigidbody> ().velocity.magnitude;
		}
		if (Input.GetMouseButtonUp (0)) {
			Throw ();
		}

	}
}
