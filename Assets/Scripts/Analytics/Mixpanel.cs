// Adapapted from: https://github.com/waltdestler/Mixpanel-Unity-CSharp

/*
 * Modifications: Isaiah Mann
 * Description: Communicates with Mixpanel API
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using LitJson;
using UnityEngine;

public static class Mixpanel
{
	// allow or prevent events from being sent while in the unity editor
	public static bool sendEventsInEditor = true;

	// Set this to your Mixpanel token.
	public static string Token;

	// Set to true to enable debug logging.
	public static bool EnableLogging = false;

	private static string distinctID;

	// Set this to the distinct ID of the current user.
	// "Lazy" implementation of a distinct ID
	// Ensures that when we pull from this we'll always have one
	public static string DistinctID
	{
		get
		{
			// Create distinctID if it has not been set yet
			if(string.IsNullOrEmpty(distinctID))
			{
				//Debug.Log("EMPTY!");

				if(!PlayerPrefs.HasKey("mixpanel_distinct_id"))
					PlayerPrefs.SetString("mixpanel_distinct_id", Guid.NewGuid().ToString());

				distinctID = PlayerPrefs.GetString("mixpanel_distinct_id");
			}
			return distinctID;
		}
	}

	// Add any custom "super properties" to this dictionary. These are properties sent with every event.
	public static Dictionary<string, object> SuperProperties = new Dictionary<string, object>();

	private const string API_URL_FORMAT = "http://api.mixpanel.com/track/?data={0}";
	private const string API_URL_PEOPLE="http://api.mixpanel.com/engage/?data={0}";
	private static MonoBehaviour _coroutineObject;

	// operationType Can be set, set_once, add, union, unset, delete
	// https://mixpanel.com/help/reference/javascript-full-api-reference
	// Scroll to "mixpanel.people.set"
	public static void SendPeople(IDictionary<string, object> peoProps, string operationType = "set")
	{
		if(string.IsNullOrEmpty(Token))
		{
			Debug.LogError("Attempted to send an event without setting the Mixpanel.Token variable.");
			return;
		}

		Dictionary<string, object> peoplesDict = new Dictionary<string, object>();
		peoplesDict.Add ("token",Token);
		if(peoProps != null)
		{
			foreach(var kvp in peoProps)
			{
				if(kvp.Value is float) // LitJSON doesn't support floats.
				{
					float f = (float)kvp.Value;
					double d = f;
					peoplesDict.Add(kvp.Key, d);
				}
				else
				{
					peoplesDict.Add(kvp.Key, kvp.Value);
				}
			}
		}
		Dictionary<string, object> jsonDict = new Dictionary<string, object>();
		jsonDict.Add("$token",Token);
		jsonDict.Add("$distinct_id",DistinctID);
		jsonDict.Add("$" + operationType, peoplesDict);
		string jsonStr = JsonMapper.ToJson(jsonDict);
		if(EnableLogging)
			Debug.Log("Sending mixpanel event: " + jsonStr);
		string jsonStr64 = EncodeTo64(jsonStr);
		string url = string.Format(API_URL_PEOPLE, jsonStr64);
		StartCoroutine(SendEventCoroutine(url));
	}

	// Call this to track charge
	public static void TrackCharge(string amount)
	{
		if(string.IsNullOrEmpty(Token))
		{
			Debug.LogError("Attempted to send an event without setting the Mixpanel.Token variable.");
			return;
		}


		Dictionary<string,object> props = new Dictionary<string, object>();
		props.Add ("$time", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"));
		props.Add("$amount", amount);
		Dictionary<string, object> moneyDict = new Dictionary<string, object>();
		moneyDict.Add ("$transactions",props);

		
		Dictionary<string, object> jsonDict = new Dictionary<string, object>();
		jsonDict.Add("$append",moneyDict);
		jsonDict.Add("$token",Token);
		jsonDict.Add("$distinct_id",DistinctID);
		string jsonStr = JsonMapper.ToJson(jsonDict);
		if(EnableLogging)
			Debug.Log("Sending mixpanel event: " + jsonStr);
		string jsonStr64 = EncodeTo64(jsonStr);
		string url = string.Format(API_URL_PEOPLE, jsonStr64);
		StartCoroutine(SendEventCoroutine(url));
	}

	// Call this to create alias
	public static void CreateAlias(string Alias)
	{
		if(string.IsNullOrEmpty(Token))
		{
			Debug.LogError("Attempted to send an event without setting the Mixpanel.Token variable.");
			return;
		}
		
		Dictionary<string, object> PropsDict = new Dictionary<string, object>();
		PropsDict.Add("distinct_id", DistinctID);
		PropsDict.Add("alias", Alias);
		PropsDict.Add("token", Token);


		Dictionary<string, object> jsonDict = new Dictionary<string, object>();
		jsonDict.Add("event", "$create_alias");
		jsonDict.Add("properties", PropsDict);
		
		string jsonStr = JsonMapper.ToJson(jsonDict);
		if(EnableLogging)
			Debug.Log("Mixpanel create alias: " + jsonStr);
		string jsonStr64 = EncodeTo64(jsonStr);
		string url = string.Format(API_URL_PEOPLE, jsonStr64);
		if(EnableLogging)
			Debug.Log("Mixpanel create alias url : " + url);
		StartCoroutine(CreateAliasCoroutine(url));
	}

	static void addCustomSuperProperties (Dictionary<string, object> propsDict) {
		propsDict.Add(
			EventList.SOUND_TOGGLED_ON_OFF,
			(SettingsUtil.FXMuted && SettingsUtil.MusicMuted) ? 
				EventList.OFF :
				EventList.ON
		);

	}

	// Call this to send an event to Mixpanel.
	// eventName: The name of the event. (Can be anything you'd like.)
	// properties: A dictionary containing any properties in addition to those in the Mixpanel.SuperProperties dictionary.
	public static void SendEvent(string eventName, IDictionary<string, object> properties = null)
	{
		if(string.IsNullOrEmpty(Token))
		{
			Debug.LogError("Attempted to send an event without setting the Mixpanel.Token variable.");
			return;
		}


		Dictionary<string, object> propsDict = new Dictionary<string, object>();
		propsDict.Add("distinct_id", DistinctID);
		propsDict.Add("token", Token);

		// Adds the custom properties for Pirate Squabbles
		addCustomSuperProperties(propsDict);

		//sets overall game play stats

		foreach(var kvp in SuperProperties)
		{
			if(kvp.Value is float) // LitJSON doesn't support floats.
			{
				float f = (float)kvp.Value;
				double d = f;
				propsDict.Add(kvp.Key, d);
			}
			else
			{
				propsDict.Add(kvp.Key, kvp.Value);
			}
		}
		if(properties != null)
		{
			foreach(var kvp in properties)
			{
				if(kvp.Value is float) // LitJSON doesn't support floats.
				{
					float f = (float)kvp.Value;
					double d = f;
					propsDict.Add(kvp.Key, d);
				}
				else
				{
					propsDict.Add(kvp.Key, kvp.Value);
				}
			}
		}
		Dictionary<string, object> jsonDict = new Dictionary<string, object>();
		jsonDict.Add("event", eventName);
		jsonDict.Add("properties", propsDict);
		string jsonStr = JsonMapper.ToJson(jsonDict);
		if(EnableLogging)
			Debug.Log("Sending mixpanel event: " + jsonStr);
		string jsonStr64 = EncodeTo64(jsonStr);
		string url = string.Format(API_URL_FORMAT, jsonStr64);
		StartCoroutine(SendEventCoroutine(url));
	}

	private static string EncodeTo64(string toEncode)
	{
		var toEncodeAsBytes = Encoding.ASCII.GetBytes(toEncode);
		var returnValue = Convert.ToBase64String(toEncodeAsBytes);
		return returnValue;
	}

	private static void StartCoroutine(IEnumerator coroutine)
	{
		if(_coroutineObject == null)
		{
			var go = new GameObject("Mixpanel Coroutines");
			UnityEngine.Object.DontDestroyOnLoad(go);
			_coroutineObject = go.AddComponent<MonoBehaviour>();
		}

		_coroutineObject.StartCoroutine(coroutine);
	}

	private static IEnumerator CreateAliasCoroutine(string url)
	{
		WWW www = new WWW(url);
		yield return www;
		if(www.error != null)
			Debug.LogWarning("Error sending creating mixpanel alias: " + www.error);
		else if(www.text.Trim() == "0")
			Debug.LogWarning("Error creating mixpanel alias: " + www.text);
		else{
			if(EnableLogging)
				Debug.Log("Mixpanel processed event: " + www.text);
		}
	}

	// Sends data to the server and yields until the server confirms it is done (or timeout)
	private static IEnumerator SendEventCoroutine(string url)
	{
		// check if we can send in editor
		bool canSend = true;
#if UNITY_EDITOR
		if(!sendEventsInEditor)
			canSend = false;
#endif
#if FORCE_DISABLE
		canSend = false;
		Debug.Log("Disabled");
#endif

		// send data to mixpanel server if we can send 
		if(canSend)
		{
			WWW www = new WWW(url);
			yield return www;
			if(www.error != null)
				Debug.LogWarning("Error sending mixpanel event: " + www.error);
			else if(www.text.Trim() == "0")
				Debug.LogWarning("Error on mixpanel processing event: " + www.text);
			else if(EnableLogging)
				Debug.Log("Mixpanel processed event: " + www.text);
		}
		else if (DebuggingUtil.Verbose)
			Debug.LogWarning("Mixpanel Event not sent, this feature is currently disabled in the unity editor. Modify the boolean sendEventsInEditor to change this.");
	}
}