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

	const string START_MUSIC = "musicStart";
	const string STOP_MUSIC = "musicStop";
	const string START_CONVERSATION = "OpenDialogue";

	public static string[] events = {
		START_MUSIC,
		STOP_MUSIC,
		START_CONVERSATION
	};
}