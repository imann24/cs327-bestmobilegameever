/*
 * Author(s): Isaiah Mann
 * An list of events as strings
 * The indexes of these events in the array correspond to the EventType enum
 */
using UnityEngine;
using System.Collections;

public static class EventList {

	public static int length {
		get {
			return events.Length;
		}
	}

	const string START_MUSIC = "menuMusicStart";
	const string STOP_MUSIC = "menuMusicStop";
	const string START_CONVERSATION = "OpenDialogue";
	public const string HIDE_TEXT_BOX = "hide();";

	public const string HELP_TEXT_BOX = "TextBox";

	public static string[] events = {
		START_MUSIC,
		STOP_MUSIC,
		START_CONVERSATION,
		HIDE_TEXT_BOX
	};
}