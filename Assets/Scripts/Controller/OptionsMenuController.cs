/*
 * Author: Isaiah Mann 
 * Description: Controller for the options menu
 */

using UnityEngine;
using System.Collections;

public class OptionsMenuController : MonoBehaviour {

	public void BackToMainMenu () {

		SceneController.LoadMainMenu();

	}
}
