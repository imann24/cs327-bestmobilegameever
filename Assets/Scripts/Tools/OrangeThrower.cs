using UnityEngine;
using System.Collections;

public class OrangeThrower : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		WindUpOrange ();
	}

	void WindUpOrange(){
		Instantiate (Resources.Load<GameObject> ("Prefabs/Orange"), transform.position, Quaternion.identity);
	}
}
