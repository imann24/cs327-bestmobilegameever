/*
 * Author: Isaiah Mann
 * Description: Used to coordinate between the different classes in the world scene
 */
using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour {
	public static WorldController Instance;
	public PlayerData SaveFile;

	void Awake () {
		init();
	}

	// Use this for initialization
	void Start () {
		EventController.Event("gameMusicStart");	
	}

	public void Save () {

		new SaveLoad().Save(SaveFile);

	}

	public void ExitToMainMenu () {

		SceneController.LoadMainMenu();

	}

	void init () {
		Instance = this;
		//TODO: Insert initialization/world loading code here
		getGameProgress();
	}

	void getGameProgress () {
		SaveFile = new SaveLoad().Load();
	}

}