/*
* Author: Isaiah Mann
* Description: Used to navigate between scenes
*/

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public static class SceneController {

	public static void LoadMainMenu () {
		LoadScene(PSScene.MainMenu);
	}

	public static void LoadMainGame () {
		LoadScene(PSScene.MainGame);
	}

	public static void LoadOptionsMenu () {
		LoadScene(PSScene.OptionsMenu);
	}

	// Uses an enum to load a scene
	// For this to work: build settings must correspond to the PSScene Enum order
	public static void LoadScene (PSScene scene) {

		SceneManager.LoadScene((int) scene);	

	}
}
