/*
 * Author(s): Isaiah Mann 
 * Description: Used to control the audio in the game
 * Is a Singleton (only one instance can exist at once)
 * Attached to a GameObject that stores all AudioSources and AudioListeners for the game
 * Dependencies: AudioFile, AudioLoader, AudioList, AudioUtil
 */
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AudioController : MonoBehaviour {
	public bool isAudioListener = true;

	// Singleton implementation
	public static AudioController Instance;

	const string path = "Audio/AudioList";
	AudioList fileList;
	AudioLoader loader;


	// Stores all the audio sources and files inside dictionaries
	Dictionary<int, AudioSource> channels = new Dictionary<int, AudioSource>();
	Dictionary<string, AudioFile> files = new Dictionary<string, AudioFile>();

	// Stores all the audio events inside dictionaries
	Dictionary<string, List<AudioFile>> playEvents = new Dictionary<string, List<AudioFile>>();
	Dictionary<string, List<AudioFile>> stopEvents = new Dictionary<string, List<AudioFile>>();

	// Audio Control Patterns
	RandomizedQueue<AudioFile> _swells;
	RandomizedQueue<AudioFile> _sweeteners;
	IEnumerator _swellCoroutine;
	IEnumerator _sweetenerCoroutine;

	// Set to false to halt active coroutines
	bool _coroutinesActive = true;
	[Header("Sweeteners")]
	public float ShortestSweetenerPlayFrequenecy;
	public float LongestSweetenerPlayFrequenecy;

	void Awake () {
		Init();
	}

	// Use this for initialization
	void Start () {
	}

	void OnDestroy () {
		// Garbage collection: otherwise events will produce null reference errors when called
		UnsubscribeEvents();
	}
		
	// The generic music loop
	public void PlayMusic () {
		EventController.Event(PSEventType.StartMusic);
	}

	public void StopMusic () {
		EventController.Event(PSEventType.StopMusic);
	}
		
	public void Play (AudioFile file) {

		Debug.Log("PLaying " + file.FileName + " " + Time.time);
		AudioSource source = GetChannel(file.Channel);

		CheckMute(file, source);

		source.clip = file.Clip;

		source.loop = file.Loop;

		source.volume = file.Volumef;

		source.Play();

	}

	public void Stop (AudioFile file) {

		if (ChannelExists(file.Channel)) {
			AudioSource source = GetChannel(file.Channel);

			if (source.clip == file.Clip) {

				source.Stop();

			}
		}

	}

	public void ToggleFXMute () {
		SettingsUtil.ToggleFXMuted (
			!SettingsUtil.fxMuted
		);
	}

	public void ToggleMusicMute () {
		SettingsUtil.ToggleMusicMuted (
			!SettingsUtil.musicMuted
		);
	}
		
	void CheckMute (AudioFile file, AudioSource source) {
		source.mute = AudioUtil.IsMuted(file.typeAsEnum);
	}

	// Checks if the AudioSource corresponding to the channel integer has been initialized
	bool ChannelExists (int channelNumber) {
		return channels.ContainsKey(channelNumber);
	}
	
	AudioSource GetChannel (int channelNumber) {
		if (channels.ContainsKey(channelNumber)) {
		
			return channels[channelNumber];

		} else {

			// Adds a new audiosource if channel is not present in dictionary
			AudioSource newSource = gameObject.AddComponent<AudioSource>();
			channels.Add(channelNumber, newSource);
			return newSource;

		}
	}


	void Init () {

		// Singleton method returns a bool depending on whether this object is the instance of the class
		if (SingletonUtil.TryInit(ref Instance, this, gameObject)) {
				
			loader = new AudioLoader(path);
			fileList = loader.Load();

			InitFileDictionary(fileList);

			AddAudioEvents();

			SubscribeEvents();

			if (isAudioListener) {
				AddAudioListener();
			}
	
		}
	}

	void InitFileDictionary (AudioList audioFiles) {
		for (int i = 0; i < audioFiles.Length; i++) {
			files.Add (
				audioFiles[i].FileName,
				audioFiles[i]
			);
		}
	}
		
	void AddAudioEvents () {

		for (int i = 0; i < fileList.Length; i++) {

			AddPlayEvents(fileList[i]);
			AddStopEvents(fileList[i]);

		}

	}

	void AddPlayEvents (AudioFile file) {
		
		for (int j = 0; j < file.EventNames.Length; j++) {

			if (playEvents.ContainsKey(file.EventNames[j])) {

				playEvents[file.EventNames[j]].Add(file);

			} else {

				List<AudioFile> files = new List<AudioFile>();
				files.Add(file);

				playEvents.Add (
					file.EventNames[j],
					files
				);

			}

		}

	}

	void AddStopEvents (AudioFile file) {

		for (int j = 0; j < file.StopEventNames.Length; j++) {

			if (stopEvents.ContainsKey(file.StopEventNames[j])) {

				stopEvents[file.StopEventNames[j]].Add(file);

			} else {

				List<AudioFile> files = new List<AudioFile>();
				files.Add(file);

				stopEvents.Add (
					file.StopEventNames[j],
					files
				);

			}

		}

	}

	void SubscribeEvents () {
		EventController.OnNamedEvent += HandleEvent;
		EventController.OnAudioEvent += HandleEvent;
	}

	void UnsubscribeEvents () {
		EventController.OnNamedEvent -= HandleEvent;
		EventController.OnAudioEvent -= HandleEvent;
	}

	void HandleEvent (string eventName) {

		if (playEvents.ContainsKey(eventName)) {

			PlayAudioList(
				playEvents[eventName]
			);
		}

		if (stopEvents.ContainsKey(eventName)) {

			StopAudioList(
				stopEvents[eventName]
			);
		}
				
	}

	void HandleEvent (AudioActionType actionType, AudioType audioType) {

		if (AudioUtil.IsMuteAction(actionType)) {

			HandleMuteAction (actionType, audioType);

		}

	}

	void HandleMuteAction (AudioActionType actionType, AudioType audioType) {
		foreach (AudioSource source in channels.Values) {

			if (fileList.GetAudioType(source.clip) == audioType) {

				source.mute = AudioUtil.MutedBoolFromAudioAction(actionType);

			}

		}
	}

	void PlayAudioList (List<AudioFile> files) {
		for (int i = 0; i < files.Count; i++) {
			Play(files[i]);
		}
	}

	void StopAudioList (List<AudioFile> files) {
		for (int i = 0; i < files.Count; i++) {
			Stop(files[i]);
		}
	}

	void AddAudioListener () {
		gameObject.AddComponent<AudioListener>();
	}

	void startTrackCycling () {
		_sweetenerCoroutine = cycleTracksFrequenecyRange(
			_sweeteners,
			ShortestSweetenerPlayFrequenecy,
			LongestSweetenerPlayFrequenecy
		);

		_swellCoroutine = cycleTracksContinuous (
			_swells
		);

		startCoroutines(
			_sweetenerCoroutine,
			_swellCoroutine
		);
	}

	public void TestCycling () {
		_sweeteners = new RandomizedQueue<AudioFile>(fileList[0], fileList[1]);
		_swells = new RandomizedQueue<AudioFile>(fileList[0], fileList[1]);
		startTrackCycling();
	}

	IEnumerator cycleTracksContinuous (RandomizedQueue<AudioFile> files) {
		while (_coroutinesActive) {	
			AudioFile nextTrack = files.Cycle();
			Play(nextTrack);
			yield return new WaitForSeconds(nextTrack.Clip.length);
		}
	}

	IEnumerator cycleTracksFrequenecy (RandomizedQueue<AudioFile> files, float frequenecy) {
		while (_coroutinesActive) {
			Play(files.Cycle());
			yield return new WaitForSeconds(frequenecy);
		}
	}

	IEnumerator cycleTracksFrequenecyRange (RandomizedQueue<AudioFile> files, float minFrequency, float maxFrequency) {
		while (_coroutinesActive) {
			Play(files.Cycle());

			yield return new WaitForSeconds(
				UnityEngine.Random.Range(
					minFrequency, 
					maxFrequency
				)
			);
		}
	}

	void startCoroutines (params IEnumerator[] coroutines) {
		for (int i = 0; i < coroutines.Length; i++) {
			StartCoroutine(coroutines[i]);
		}
	}

}
