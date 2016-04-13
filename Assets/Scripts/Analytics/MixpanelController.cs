// Adapated from: https://github.com/waltdestler/Mixpanel-Unity-CSharp
/*
 * Modifier: Isaiah Mann
 * All the game specific MixPanel events are kept here
 * All of the generic MixPanel calls are kept in Mixpanel.cs
 */

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

// Mixpanel events are sent through this class
// Note that this class currently uses PlayerPrefs for all saving - it is not recommended to do this for any user sensitive data
public class MixpanelController : MonoBehaviour
{

	// Assign to this in the inspector
	// TODO: keep this updated for each build to retain accurate analytics
	public string versionNumber;

	// Singleton implementation
	public static MixpanelController instance;



	void Awake()
	{
		// singleton setup - don't destroy this when loading
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		
			// Subscribes to all events
			LinkToEvents();
		}
		else
		{
			GameObject.Destroy(this.gameObject);
		}

	}

	void Start()
	{
		// Set Mixpanel Token: this is project specific
		Mixpanel.Token = "cf2fd92b4a900770f961e65208bbb5c4";

		// Not sure if this is required or not, clears all super properties in the SuperProperties dictionary
		Mixpanel.SuperProperties.Clear();
		// Quick way to check if this is the first usage
		// Generally put information you care about only recording once in here (install date, version on install, etc)
		// See Mixpanel.SendPeople and look into set_once
		if(PlayerPrefs.GetInt("FirstUse") == 0)
		{	
			PlayerPrefs.SetInt("FirstUse", 1);
			FirstUse(DateTime.Now.ToString(), Mixpanel.DistinctID);
		}

		// Note that versionNumber is not static, this enables it to be set through the inspector - but also means we have to pull the instance
		AddSuperProperties ("Version", MixpanelController.instance.versionNumber);
	
	}

	//updates the playerprefs count of time to track between game sessions
	void OnLevelWasLoaded (int level) {
		
	}

	void Destroy () {
		// Unsubscribes from event calls
		UnlinkFromEvents ();
	}
	
	//establishes the in game event references that send MixPanel events
	void LinkToEvents () {
		EventController.OnNamedEvent += handleNamedEvents;	
		EventController.OnNamedTextEvent += handleNamedTextEvents;
	}



	//unlinks from the game event references
	void UnlinkFromEvents () {
		EventController.OnNamedEvent -= handleNamedEvents;	
		EventController.OnNamedTextEvent -= handleNamedTextEvents;
	}

	void handleNamedEvents (string eventName) {
		switch (eventName) {

		case EventList.MATEY_BUTTON_CLICKED:
			sendSimpleNamedEvent(eventName);
			break;

		case EventList.PLAY_BUTTON_CLICKED:
			sendSimpleNamedEvent(eventName);
			break;

		case EventList.SOUND_TOGGLED_ON_OFF:
			sendSimpleNamedEvent(eventName);
			break;

		case EventList.GAME_END_SCREEN_REACHED:
			sendSimpleNamedEvent(eventName);
			break;

		}
	}

	void handleNamedTextEvents (string eventName, string text) {
		InventoryReport report;

		switch (eventName) {


		case EventList.INVENTORY_ITEM_COLLECTED:
			report = GameManager.InventoryManager.GetReport();
			report.AddItemCollected(text);
			sendInventoryEvent(eventName, report);
			break;

		case EventList.INVENTORY_ITEM_DESTROYED:
			report = GameManager.InventoryManager.GetReport();
			report.AddItemDestroyed(text);
			sendInventoryEvent(eventName, report);
			break;


		}
	}

	// Send an empty event with no added properties
	void sendSimpleNamedEvent (string eventName) {
		Mixpanel.SendEvent (
			eventName,
			null
		);
	}

	// Send an inventory event (containing a full status on the inventory
	// TODO: Add an inventory report class: containing fall status of inventory in a dict
	void sendInventoryEvent (string eventName, InventoryReport inventoryReport) {
		Mixpanel.SendEvent (
			eventName,
			inventoryReport.Get()
		);
	}

	#region Example event 
	// Example event
	// Parameters for the function are a handy way to have an event take in multiple Properties
	public static void GamePlay(bool isReplay)
	{
		Mixpanel.SendEvent("Game Play", new Dictionary<string, object>{
			{"Replay", isReplay},
			});
	}
	#endregion 


	#region People Properties

	// Exampe people property
	// Common setup for the first use
	private static void FirstUse(string date, string distinct_id)
	{
		Mixpanel.SendPeople(new Dictionary<string ,object>{
			{"First Use", date},
			{"distinct_id", distinct_id},
		}, "set_once");
	}

	#endregion


	#region Super Properties
	//Add or Remove SuperProperties

	static public void AddSuperProperties(string propertyName, string propertyValue)
	{
		Mixpanel.SuperProperties.Add(propertyName, propertyValue);
	}

	static public void RemoveSuperProperties(string property)
	{
		Mixpanel.SuperProperties.Remove(property);
	}
	#endregion

	
	#region Helper Functions
	// Converts a float into a string that fits the format Minutes:Seconds:MilliSeconds
	public static string ConvertFloatToTimeString(float _time)
	{
		TimeSpan time = TimeSpan.FromSeconds(_time);
		return string.Format("{0:D2}:{1:D2}:{2:D2}", time.Minutes, time.Seconds, time.Milliseconds);
	}
	#endregion
}