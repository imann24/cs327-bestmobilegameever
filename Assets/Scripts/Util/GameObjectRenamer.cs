using UnityEngine;
using System.Collections;

public class GameObjectRenamer : MonoBehaviour {

	public string Name = "DefaultName";

	// Use this for initialization
	void Start () {
		gameObject.name = Name;
	}
}
