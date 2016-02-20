/*
 * Author: Isaiah Mann
 * Description: Used to coordinate between the different classes in the world scene
 */
using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour {
	public static WorldController Instance;
	public ConversationDisplayController DialogueDisplay;

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

	void init () {
		Instance = this;
		//TODO: Insert initialization/world loading code here
		DialogueDisplay.Init();
	}
}
