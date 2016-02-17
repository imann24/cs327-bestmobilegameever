/*
* Author: Isaiah Mann
* Description: Used to navigate the main menu
*/

using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	public void LaunchGame () {
		SceneController.LoadMainGame();
	}

}
