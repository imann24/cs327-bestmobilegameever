using UnityEngine;
using System.Collections;

public class NoahMove : MonoBehaviour {

	private Vector3 destination;
	private NavMeshAgent navAgent;


	// Use this for initialization
	void Start () {
		navAgent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void GoTo(Vector3 targetPoint){
		navAgent.SetDestination(targetPoint);
	}
}
