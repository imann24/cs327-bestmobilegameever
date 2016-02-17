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

	public static void LoadScene (PSScene scene) {

		SceneManager.LoadScene((int) scene);	

	}
}
