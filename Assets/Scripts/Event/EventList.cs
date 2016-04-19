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

	#region MixPanel Events

	public const string PLAY_BUTTON_CLICKED = "Play Button Clicked";
	public const string MATEY_BUTTON_CLICKED = "Matey Button Clicked";
	public const string GAME_END_SCREEN_REACHED = "Game End Screen Reached";
	public const string SOUND_TOGGLED_ON_OFF = "Sound Toggled On/Off";
	public const string OFF = "Off";
	public const string ON = "On";
	public const string INVENTORY_ITEM_DESTROYED = "Inventory Item Destroyed";
	public const string INVENTORY_ITEM_COLLECTED = "Inventory Item Collected";

	#endregion


	#region Special Actions

	public const string END_DEMO = "EndDemo";

	#endregion

	public static string[] events = {
		START_MUSIC,
		STOP_MUSIC,
		START_CONVERSATION,
		HIDE_TEXT_BOX
	};
}