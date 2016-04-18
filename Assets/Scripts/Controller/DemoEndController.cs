/*
 * Author: Isaiah Mann
 * Description: Controls the 
 */

using UnityEngine;
using System.Collections;

public class DemoEndController : MonoBehaviour {
	public string WebsiteURL = "http://piratesquabbles.com/";

	// Use this for initialization
	void Start () {
		init();	
	}

	public void LoadMainMenu () {
		SceneController.LoadMainMenu();
	}

	public void LoadWebsite () {
		Application.OpenURL(
			WebsiteURL
		);
	}

	void init () {
		EventController.Event(EventList.GAME_END_SCREEN_REACHED);
	}
}
