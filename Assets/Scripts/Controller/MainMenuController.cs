/*
* Author: Isaiah Mann
* Description: Used to navigate the main menu
*/

using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {
	static bool FIRST_LOAD = true;

	void Start () {
		if (FIRST_LOAD) {
			FIRST_LOAD = false;
			EventController.Event("menuMusicStart");
		}
	}

	public void LaunchGame () {

		SceneController.LoadMainGame();

	}

	public void LaunchTutorial () {

		SceneController.LoadTutorialScene();

	}

	public void LoadOptionsMenu () {

		SceneController.LoadOptionsMenu();

	}

}