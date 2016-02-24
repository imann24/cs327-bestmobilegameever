/*
 * Author: Isaiah Mann
 * Description: Used to coordinate between the different classes in the world scene
 */
using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour {
	public static WorldController Instance;
	public ConversationDisplayController DialogueDisplay;
	public PlayerData SaveFile;

	public bool DialogueActive {
		get {
			if (ConversationDisplayController.Instance == null) {
				return false;
			} else {
				return ConversationDisplayController.Instance.Active;
			}
		}
	}

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
		DialogueDisplay.Init();
		getGameProgress();
	}

	void getGameProgress () {
		SaveFile = new SaveLoad().Load();
	}

}