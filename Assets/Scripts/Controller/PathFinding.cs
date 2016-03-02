using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinding : MonoBehaviour {

	public static int nodesNumber = 15	;

	public GameObject[] nodes = new GameObject[nodesNumber];

	public List<GameObject> path = new List<GameObject>();
	public GameObject currentNode;
	private bool hasPath = false;

	public static PathFinding Instance
	{
		get
		{
			return instance;
		}
	}
	private static PathFinding instance = null;

	void Awake (){
		if(instance){
			DestroyImmediate(gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad(gameObject);
	}


	public void MakeAPathTo (GameObject targetNode){
		if (!PlayerMovement.Instance.moving) {
			if (targetNode != currentNode) {
				StartCoroutine ("FindPath", targetNode);
			}
		}
	}

	IEnumerator FindPath(GameObject targetNode){
		GameObject nextNode = currentNode;
		bool done = false;
		while (!done) {
			for (int i = 0; i < nodes.Length; i++) {
				if (Vector3.Distance (targetNode.transform.position, nodes[i].transform.position)
				    < Vector3.Distance (targetNode.transform.position, nextNode.transform.position)) {
					nextNode = nodes [i];
					path.Add (nextNode);
				}
			}

			if (nextNode == targetNode) {
				done = true;
			}
			Debug.Log (nextNode.name);
			yield return null;
		}
		currentNode = nextNode;
		hasPath = true;
		StartCoroutine ("WalkPath");
	}

	IEnumerator WalkPath(){
		while (hasPath) {
			if (!PlayerMovement.Instance.moving) {
				for (int i = 0; i < path.Count; i++) {
					PlayerMovement.Instance.MoveTowards (path[i].transform.position, false);
					while (PlayerMovement.Instance.moving) {
						yield return null;
					}
				}
			}
			hasPath = false;
			yield return null;
		}
		path.Clear ();
	}
}
