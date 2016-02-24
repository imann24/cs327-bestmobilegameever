using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour {

	GameObject player { get { return GameManager.Instance.playerCharacter; } }
	Vector3 destination;
	public float angleThreshold = 20;

	void Start () {
		destination = transform.position;
	}

	float GetAngleToPlayer(){
		Vector3 vecToPlayer = player.transform.position - transform.position;
		Vector3 xzToPlayer = Vector3.ProjectOnPlane (vecToPlayer, Vector3.up);
		Debug.DrawRay (transform.position, Vector3.forward);
		Debug.DrawRay (transform.position, xzToPlayer);
		return Vector3.Angle (Vector3.forward, xzToPlayer);
	}

	void LateUpdate () {//*
		if (GetAngleToPlayer() > angleThreshold) {
			destination = new Vector3 (player.transform.position.x, transform.position.y, transform.position.z);
		}
		if (destination != transform.position) {
			transform.position = Vector3.MoveTowards (transform.position, destination, player.GetComponent<Movement> ().maxVelocity * 0.75f * Time.deltaTime);
		}//*/
		//gameObject.GetComponent<Rigidbody> ().MovePosition (player.transform.position);
	}
}
