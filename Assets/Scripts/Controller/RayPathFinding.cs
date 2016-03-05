using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RayPathFinding : MonoBehaviour {
	public bool _debug = false;

	public List<Vector2> points = new List<Vector2>();
	public bool hasPath = false;

	public void GetPath(Vector2 endPoint){
		hasPath = false;
		StopAllCoroutines ();
		StartCoroutine ("MakePath", endPoint);
	}

		

//	RaycastHit2D hit = Physics2D.Linecast (startPoint, endPoint, 7);
//	Debug.DrawLine (startPoint, endPoint,Color.blue, 1.5f);
//	if (hit) {


	Vector2 GetClosestCorner(Vector2 playerPos, RaycastHit hit){
		Vector2 corner = hit.point;
		Vector2 topLeft = new Vector2 (hit.transform.position.x - hit.collider.bounds.size.x, hit.transform.position.y + hit.collider.bounds.size.y);
		Vector2 topRight = new Vector2 (hit.transform.position.x + hit.collider.bounds.size.x, hit.transform.position.y + hit.collider.bounds.size.y);
		Vector2 bottomLeft = new Vector2 (hit.transform.position.x - hit.collider.bounds.size.x, hit.transform.position.y - hit.collider.bounds.size.y);
		Vector2 bottomRight = new Vector2 (hit.transform.position.x + hit.collider.bounds.size.x, hit.transform.position.y - hit.collider.bounds.size.y);

		bool doneTL = false;
		bool doneTR = false;
		bool doneBL = false;
		bool doneBR = false;

		foreach (Vector2 pathPoint in points) {
			if (pathPoint == topLeft) {
				doneTL = true;
			}
			if (pathPoint == topRight) {
				doneTR = true;
			}
			if (pathPoint == bottomLeft) {
				doneBL = true;
			}
			if (pathPoint == bottomRight) {
				doneBR = true;
			}
		}

		if ((playerPos.x < hit.point.x) && !doneTL && !doneBL) { // If on left
			if (Vector2.Distance(hit.point, topLeft) >= Vector2.Distance (hit.point, bottomLeft)){
				corner = topLeft;
			}else{
				corner = bottomLeft; 
			}
		}
		else if ((playerPos.x > hit.point.x) && !doneTR && !doneBR) { // If on right
			if (Vector2.Distance(hit.point, topRight) >= Vector2.Distance (hit.point, bottomRight)){
				corner = topRight; 
			}else{
				corner = bottomRight;
			}
		}
		else if ((playerPos.y < hit.point.y) && !doneBL && !doneBR) { // If on bottom
			if (Vector2.Distance(hit.point, bottomLeft) >= Vector2.Distance (hit.point, bottomRight)){
				corner = bottomLeft;
			}else{
				corner = bottomRight;
			}
		} 
		else if ((playerPos.y > hit.point.y) && !doneBR && !doneBL) { // If on top
			if (Vector2.Distance(hit.point,topLeft) >= Vector2.Distance (hit.point, topRight)){
				corner = topLeft;
			}else{
				corner = topRight;
			}
		}

		return corner;
	}

	IEnumerator MakePath (Vector2 endPoint){
		points.Clear ();
		bool done = false;
		Vector2 startPoint = PlayerMovement.Instance.transform.position;
		while (!done) {
			RaycastHit hit;
			if (_debug) {
				Debug.DrawLine (startPoint, endPoint, Color.blue, 1.5f);
			}
			if (Physics.Linecast (startPoint, endPoint, out hit)) {
				if (hit.transform.CompareTag("Boundary")) {
					Vector2 point = GetClosestCorner (startPoint, hit);
					points.Add (point);
					startPoint = point;
				} 
			}else {
				points.Add (endPoint);
				done = true;
			}
			yield return null;
		}
		hasPath = true;
		StartCoroutine ("WalkPath");
	}

	IEnumerator WalkPath(){
		while (hasPath) {
			if (!PlayerMovement.Instance.moving) {
				for (int i = 0; i < points.Count; i++) {
					PlayerMovement.Instance.MoveTowards (points[i], false);
					while (PlayerMovement.Instance.moving) {
						yield return null;
					}
				}
			}
			hasPath = false;
			yield return null;
		}
		points.Clear ();
	}
}
