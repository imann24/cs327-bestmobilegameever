using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour {

	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;
	
	// Update is called once per frame
	void Update () {
		if (target) {
			transform.position = Vector3.SmoothDamp (transform.position, new Vector3 (target.position.x, transform.position.y, target.position.z), ref velocity, dampTime);
		}
	}
}
