/*
* Author: Isaiah Mann
* Description: Used to navigate the main menu
*/

using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {
	static bool FIRST_LOAD = true;

	public GameObject NewGameButton;
	public GameObject NewGameConfirmationPanel;

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

	public void LoadDevelopScenesList () {

		SceneController.LoadDevelopSceneListScene();

	}

	public void ShowConfirmationPanel(){
		NewGameConfirmationPanel.SetActive (true);
	}

	public void HideConfirmationPanel(){

		NewGameConfirmationPanel.SetActive (false);
	}

	public void StartNewGame(){

		new SaveLoad().ClearSave(); 

		LaunchGame();

	}


	public void LoadCreditsMenu () {
		SceneController.LoadCredits();
	}

}