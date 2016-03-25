/*
 * Author: Isaiah Mann 
 * Description: Controller for the options menu
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsMenuController : MonoBehaviour {
	
	public GameObject ResetGameButton;
	public GameObject ConfirmationPanel;
	public void Start()
	{
		if (new SaveLoad ().HasSaveData ()) { 
			ResetGameButton.GetComponent<Button> ().interactable = true; 
		} else {
			ResetGameButton.GetComponent<Button> ().interactable = false; 
		}
	}




	public void BackToMainMenu () {
		SceneController.LoadMainMenu();


	}

	public void CreditsScene () {

		SceneController.LoadCredits ();
	}

	public void ResetSaveFile () {


		SceneController.LoadMainMenu ();
		new SaveLoad ().ClearSave ();
	
	}

	public void ShowConfirmationPanel()
	{
		ConfirmationPanel.SetActive (true);
	}


}
