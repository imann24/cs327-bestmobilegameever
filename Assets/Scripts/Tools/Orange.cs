using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Orange : MonoBehaviour{

	bool isAiming = true;
	public float maxWindup = 4;
	public float lifeSpan = 20;
	float lastVelocitySquared = 0;
	bool hitSomething = false;
	Rigidbody rigidBody;
	SpringJoint springJoint;
	GameObject thrower;
	// Use this for initialization
	void Start () {
		rigidBody = gameObject.GetComponent<Rigidbody> ();
		springJoint = gameObject.GetComponent<SpringJoint> ();
	}

	public static GameObject WindUp(GameObject orangeThrower){
		GameObject go = Instantiate (Resources.Load<GameObject> ("Prefabs/Orange"), orangeThrower.transform.position, Quaternion.identity) as GameObject;
		go.GetComponent<Orange> ().thrower = orangeThrower;
		go.GetComponent<SpringJoint> ().connectedBody = orangeThrower.GetComponent<Rigidbody> ();
		orangeThrower.GetComponent<Rigidbody> ().isKinematic = true;
		orangeThrower.GetComponent<CharacterController> ().enabled = false;
		return go;
	}

	void Aim(){
		//the orange should follow the pointer, but not farther away than the max windup, 
		//so the throwerPlane and mouseCast ray show where it would be, and then move towards makes it not exceed maxWindup
		Ray mouseCast = Camera.main.ScreenPointToRay (Input.mousePosition);
		Debug.DrawRay (mouseCast.origin, mouseCast.direction);
		Plane throwerPlane = new Plane (thrower.transform.position, thrower.transform.position + Vector3.left, thrower.transform.position + Vector3.up);
		float alongCast;
		throwerPlane.Raycast (mouseCast, out alongCast);
		transform.position = Vector3.MoveTowards (thrower.transform.position, mouseCast.GetPoint (alongCast), maxWindup);
	}

	void OnCollisionEnter(Collision collision){
		Interactable interactableHit = collision.gameObject.GetComponent<Interactable> ();
		//we don't want the orange triggering multiple times per throw, especially not on the same thing. It also shouldn't trigger
		if (interactableHit != null && hitSomething == false) {
			interactableHit.OnOrange ();
		}
		hitSomething = true;//once it collides with anything, the throw is spent. It shouldn't trigger if it rolls into something.
	}
		
	void Update () {
		if (isAiming) {//while we're aiming, aim
			Aim ();
		} else if (springJoint != null){//until we remove the spring
			if (lastVelocitySquared > rigidBody.velocity.sqrMagnitude) {//check if the orange is slowing down (i.e. the spring is now pulling in the wrong direction)
				Destroy (springJoint);//if it is, destroy it so the orange flys freely
				thrower.GetComponent<Rigidbody> ().isKinematic = false;//and restore the thrower to regular kinematics.
				thrower.GetComponent<CharacterController>().enabled = true;
			}
			lastVelocitySquared = rigidBody.velocity.sqrMagnitude;//if the orange isn't slowing down yet, update the variable used to track whether it's slowing down
		} else {//after we remove the spring, start a countdown to automatically kill the orange so it doesn't clog the data.
			lifeSpan -= Time.deltaTime;
			if (lifeSpan <= 0) {
				Destroy (gameObject);
			}
		}

		if (Input.GetMouseButtonUp (0)) {
			//don't throw the orange if it's released too close to the thrower. Use a raycast to determine if the pointer is being released while over the thrower.
			//If it is, destroy the orange instead. later change this to give the orange back to the player.
			Ray mouseCast = Camera.main.ScreenPointToRay (Input.mousePosition);
			Plane throwerPlane = new Plane (thrower.transform.position, thrower.transform.position + Vector3.left, thrower.transform.position + Vector3.up);
			if (Physics.RaycastAll(mouseCast).ToList ().FindAll (hit => hit.collider.gameObject == thrower).Count > 0) {
				Destroy (gameObject);
			} else {
				rigidBody.isKinematic = false;
				isAiming = false;
			}
		}
	}
}
