using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public const bool DEBUGGING = true;
	// Use this for initialization
	void Start () {
		Instantiate(Resources.Load<GameObject>("Prefabs/UserInterface"));
		InventoryManager im = InventoryManager.Instance;
		TagManager tm = TagManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SaveGame(){
		if (GameManager.DEBUGGING) {
			Debug.Log ("Saved Game!");
		}
	}

	public void QuitGame(){
		if (GameManager.DEBUGGING) {
			Debug.Log ("Quit Game!");
		}
	}
}
