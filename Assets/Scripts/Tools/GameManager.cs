using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public const bool DEBUGGING = true;
	public AudioController AudioManager;
	public GameObject playerCharacter;
	public static GameManager Instance;
	public List<Interaction> defaults { get; private set; }

	void Awake () {
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
		//gameObject.AddComponent<InputManager> ();
	}

	// Use this for initialization
	void Start () {
		Instantiate(Resources.Load<GameObject>("Prefabs/UserInterface"));
		InventoryManager im = InventoryManager.Instance;
		TagManager tm = TagManager.Instance;
		AudioManager = Instantiate(Resources.Load<GameObject>("Prefabs/AudioController")).GetComponent<AudioController>();
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

	public void ClearSave(){
		new SaveLoad ().ClearSave ();
	}
}
