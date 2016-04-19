/*
* Author: Isaiah Mann
* Description: Used to navigate the main menu
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuController : MonoBehaviour {
	static bool FIRST_LOAD = true;

	public GameObject NewGameButton;
	public GameObject NewGameConfirmationPanel;
	public GameObject LaunchGameButton;

	void Start () {
		if (FIRST_LOAD) {
			FIRST_LOAD = false;
			EventController.Event("menuMusicStart");

		}
		/**
		if (new SaveLoad ().HasSaveData ()) { 
			LaunchGameButton.GetComponent<Button> ().interactable = true; 
		} else {
			LaunchGameButton.GetComponent<Button> ().interactable = false; 
		} **/

	}

	public void LaunchGame () {
		
		SceneController.LoadMainGame();
	} 


	public void LaunchTutorial () {
		EventController.Event(EventList.PLAY_BUTTON_CLICKED);
		SceneController.LoadTutorialScene();
	}
		
	public void LoadOptionsMenu () {

		SceneController.LoadOptionsMenu();

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