using UnityEngine;
using System.Collections;

public class SimpleMovement : MonoBehaviour {
	public float moveSpeed = 5f;
	private bool moving = false;

	public void Move(Vector2 location){
		if (!moving) {
			StopAllCoroutines ();
			StartCoroutine ("MoveTo", location);
		}
	}
	IEnumerator MoveTo(Vector2 endLocation){
		moving = true;
		while (Vector2.Distance(transform.position, endLocation) > 0.1f) {
			
			transform.position = Vector2.MoveTowards (transform.position, endLocation, moveSpeed * Time.deltaTime);

			yield return new WaitForFixedUpdate();
		}
		moving = false;
	}
}
