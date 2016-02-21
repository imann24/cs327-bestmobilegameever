/*
 * Author: Isaiah Mann
 * Description: Used to control the visual display class
 * Notes: Should be attached to ConversationDisplayerCanvas
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConversationDisplayController : MonoBehaviour {

	public bool AutoHide = true;
	public bool Active {
		get {
			return gameObject.activeSelf;
		}
	}

	public Text CharacterName;
	public Text LineOfDialogue;

	public Image CharacterPortraitLeft;
	public Image CharacterPortraitRight;

	public ResponseDisplayController ResponseDisplay;

	Conversation _conversation;

	public static ConversationDisplayController Instance;


	void Awake () {
		Init();
	}

	public void Show () {
		gameObject.SetActive(true);
	}

	public void Hide () {
		gameObject.SetActive(false);
	}
		
	public void SetCharacter (string characterName, ScreenPosition position, Sprite characterPortrait = null) {
		Image portraitFrame = null;

		switch (position) {

		case ScreenPosition.Left:
			portraitFrame = CharacterPortraitLeft;
			break;
		
		case ScreenPosition.Right:
			portraitFrame = CharacterPortraitRight;
			break;
		}

		if (characterPortrait != null) {
			portraitFrame.sprite = characterPortrait;
		}

		TogglePortraits(position);

		SetName(characterName);
	}

	public void TogglePortraits (ScreenPosition position) {
		switch(position) {

		case ScreenPosition.Left:
			CharacterPortraitLeft.enabled = true;
			CharacterPortraitRight.enabled = false;
		break;

		case ScreenPosition.Right:
			CharacterPortraitLeft.enabled = false;
			CharacterPortraitRight.enabled = true;
		break;

		}
	}

	public void StartConversation (string conversationFilePathInResources, ScreenPosition position = ScreenPosition.Right) {
		EventController.Event(PSEventType.StartConversation);

		Show();

		SetConversation (
			new Conversation(conversationFilePathInResources),
			position
		);
	}

	public void SetConversation (Conversation conversation, ScreenPosition position = ScreenPosition.Right) {
		this._conversation = conversation;
		SetText();
		SetCharacter(
			this._conversation.GetCurrentSpeaker(),
			position
		);
	}

	public void AdvanceConversation () {
		if (_conversation != null) {

			if (_conversation.HasNext()) {

				EventController.Event(PSEventType.StartConversation);

				_conversation.AdvanceDialogue();

				SetText();

			} else {
				
				//Automatically closes the UI if the conversation is over
				Hide();

			}
		}
	}

	public void SetName (string name) {
		CharacterName.text = name;
	}

	public void SetText (string text) {
		LineOfDialogue.text = text;


	}

	public void SetResponses (string [] responses) {
		ResponseDisplay.Show(responses);
	}

	public void SetText () {
		if (_conversation != null) {
			SetText(_conversation.GetCurrentDialogue());
			SetResponses(_conversation.GetCurrentResponses());
			SetName(_conversation.GetCurrentSpeaker());
		}
	}

	public void Init () {
		if (AutoHide) {
			Hide();
		}

		Instance = this;
	}
}
