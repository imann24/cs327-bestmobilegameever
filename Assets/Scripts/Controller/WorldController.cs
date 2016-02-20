using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		EventController.Event("gameMusicStart");	
	}
}
