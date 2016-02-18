using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public float moveSpeed = 1f;

	public bool moving = false;

	private bool flipped = false;
	void Flip(){
		flipped = !flipped;
		transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
	}

	public void MoveTowards(Vector2 location, bool person){
		if (!moving) {
			if (person) {
				if (location.x > transform.position.x) {
					location = new Vector2 (location.x - 1f, location.y);
				} else {
					location = new Vector2 (location.x + 1f, location.y);
				}
			}
			if (location.x > transform.position.x) {
				if (flipped) {
					Flip ();
				}
			} else {
				if (!flipped) {
					Flip ();
				}
			}
			StartCoroutine ("MoveLerp", location);
		}
	}

	IEnumerator MoveLerp(Vector2 endLocation){
		moving = true;
		float percentComplete = 0f;
		while (Vector2.Distance(transform.position, endLocation) > 0.1f) {

			percentComplete += Time.deltaTime * moveSpeed;

			transform.position = Vector2.MoveTowards (transform.position, endLocation, moveSpeed * Time.deltaTime);

			yield return null;
		}
		moving = false;
		Debug.Log ("Done walking");
	}
}
