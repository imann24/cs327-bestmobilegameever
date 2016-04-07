/*
 * Author: Isaiah Mann
 * Description: Subscribes events to set the text of the help text box in a certain way
 */

using UnityEngine;
using System.Collections;

public class HelpTextBox : TextBox {

	protected override void subscribeEvents () {
		EventController.OnNamedTextEvent += handleNamedTextEvent;
		EventController.OnNamedEvent += handleNamedEvent;
	}

	protected override void unsubscribeEvents () {
		EventController.OnNamedTextEvent -= handleNamedTextEvent;
		EventController.OnNamedEvent -= handleNamedEvent;
	}

	void OnLevelWasLoaded (int level) {
		Hide();
	}

	void handleNamedTextEvent (string key, string text) {
		if (isHelpTextBoxEvent(key)) {
			Show();
			Set(text);
		}
	}

	void handleNamedEvent (string eventName) {
		if (eventName == EventList.HIDE_TEXT_BOX) {
			Hide();
		}
	}

	bool isHelpTextBoxEvent (string key) {
		return key == EventList.HELP_TEXT_BOX;
	}
}
