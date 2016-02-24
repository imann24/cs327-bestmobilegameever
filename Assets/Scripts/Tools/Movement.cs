using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float maxVelocity = 5;
	float heightToCenter { get { return gameObject.GetComponent<Collider> ().bounds.extents.y; } }
	Vector3 baseToCenter { get { return Vector3.up * heightToCenter; } }
	Vector3 floorCenter { get { return transform.position - baseToCenter; } }
	public Vector3 destination; //{ get; private set; }


	void Start () {
		Ground ();
	}

	void Ground(){
		Ray down = new Ray (transform.position, Vector3.down);
		RaycastHit hit;
		if (Physics.Raycast (down, out hit)) {
			transform.position = hit.point + baseToCenter;
			destination = hit.point;
		}
	}

	void FixedUpdate () {
		if (Vector3.Distance (destination, floorCenter) < 0.1f)
			GetComponent<Rigidbody> ().velocity = Vector3.zero;
	}

	void OnCollisionEnter(Collision collision){
		GetComponent<Rigidbody> ().velocity = Vector3.zero;
	}

	public void MoveTo(Vector3 target){
		destination = target;
		GetComponent<Rigidbody> ().velocity = Vector3.zero;
		GetComponent<Rigidbody> ().AddForce (Vector3.MoveTowards(floorCenter,destination,maxVelocity) - floorCenter, ForceMode.VelocityChange);
	}
}
