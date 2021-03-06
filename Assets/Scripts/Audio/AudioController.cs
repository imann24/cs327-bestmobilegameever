﻿/*
 * Author(s): Isaiah Mann 
 * Description: Used to control the audio in the game
 * Is a Singleton (only one instance can exist at once)
 * Attached to a GameObject that stores all AudioSources and AudioListeners for the game
 * Dependencies: AudioFile, AudioLoader, AudioList, AudioUtil, RandomizedQueue<AudioFile>, EventList, PSScene
 */
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AudioController : MonoBehaviour {
	public bool isAudioListener = true;
	public float Volume = 1.0f;

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
	RandomizedQueue<AudioFile> _GUIclicks;
	RandomizedQueue<AudioFile> _matey;
	RandomizedQueue<AudioFile> _ambienceMain;
	RandomizedQueue<AudioFile> _ambienceTutorial;
	RandomizedQueue<AudioFile> _swabbie;
	RandomizedQueue<AudioFile> _firstMate;
	RandomizedQueue<AudioFile> _quarterMaster;
	RandomizedQueue<AudioFile> _rigger;
	RandomizedQueue<AudioFile> _secondMate;
	RandomizedQueue<AudioFile> _swabbieSpeech;
	RandomizedQueue<AudioFile> _oj;
	RandomizedQueue<AudioFile> _sadieAhoy;
	RandomizedQueue<AudioFile> _sadieTalk;
	RandomizedQueue<AudioFile> _shipmaster;
	IEnumerator _swellCoroutine;
	IEnumerator _sweetenerCoroutine;
	IEnumerator _ambienceTutorialCoroutine;
	IEnumerator _ambienceMainCoroutine;
	IEnumerator _swabbieCoroutine;

	// Set to false to halt active coroutines
	bool _coroutinesActive = true;
	[Header("Sweeteners")]
	public float ShortestSweetenerPlayFrequenecy = 10;
	public float LongestSweetenerPlayFrequenecy = 25;


	void Awake () {
		Init();
	}
		
	void Start () {
	}

	void OnDestroy () {
		// Garbage collection: otherwise events will produce null reference errors when called
		UnsubscribeEvents();
	}
		

	void OnLevelWasLoaded (int level) {
		if ((PSScene)level == PSScene.MainMenu) {
			StopTrackCycling();
			PlayMainMenuMusic();
			StopCoroutine (_ambienceTutorialCoroutine);
			StopCoroutine (_ambienceMainCoroutine);
		} else if ((PSScene)level == PSScene.MainGame) {
			StopMainMenuMusic();
			StopTrackCycling();
			StartTrackCycling();
			StopCoroutine (_ambienceTutorialCoroutine);
		}else if ((PSScene)level == PSScene.TutorialScene) {
			StopMainMenuMusic();
			StartTrackCycling();
		} else {
		}
	}

	// The generic music loop
	public void PlayMainMenuMusic () {
		EventController.Event(PSEventType.StartMusic);
	}

	public void StopMainMenuMusic () {
		EventController.Event(PSEventType.StopMusic);
	}
		
	public void Play (AudioFile file) {
	
		AudioSource source = GetChannel(file.Channel);

		CheckMute(file, source);

		source.clip = file.Clip;

		source.loop = file.Loop;

		source.volume = file.Volumef * Volume;

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
			!SettingsUtil.FXMuted
		);
	}

	public void ToggleMusicMute () {
		SettingsUtil.ToggleMusicMuted (
			!SettingsUtil.MusicMuted
		);
	}

	public void ChangeVolume (float newVolume) {

		this.Volume = newVolume;

		adjustSourceVolume();

	}
		
	void adjustSourceVolume () {

		foreach (AudioSource source in channels.Values) {
			AudioFile file = fileList.GetAudioFileByClip(source.clip);
			if (file != null) {
				source.volume = file.Volumef * this.Volume;
			}

		}

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


	// Must be colled to setup the class's functionality
	void Init () {

		// Singleton method returns a bool depending on whether this object is the instance of the class
		if (SingletonUtil.TryInit (ref Instance, this, gameObject)) {
				
			loader = new AudioLoader (path);
			fileList = loader.Load ();
			fileList.Init ();

			InitFileDictionary (fileList);

			AddAudioEvents ();

			SubscribeEvents ();

			if (isAudioListener) {
				AddAudioListener ();
			}
			initCyclingAudio ();
	
		} else { 
			//this = Instance;
		}
	}

	void InitFileDictionary (AudioList audioFiles) {
		for (int i = 0; i < audioFiles.Length; i++) {
			try {
				files.Add (
					audioFiles[i].FileName,
					audioFiles[i]
				);
			} catch {
				Debug.Log(audioFiles[i].FileName + " already exists in the dictionary");
			}
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

	// Uses C#'s delegate system
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

	// Used to loop through lists of tracks in pseudo-random order
	public void StartTrackCycling () {
		_sweetenerCoroutine = cycleTracksFrequenecyRange(
			_sweeteners,
			ShortestSweetenerPlayFrequenecy,
			LongestSweetenerPlayFrequenecy
		);

		_swellCoroutine = cycleTracksContinuous (
			_swells
		);

		_ambienceMainCoroutine = cycleTracksContinuous (
			_ambienceMain
		);

		_ambienceTutorialCoroutine = cycleTracksContinuous (
			_ambienceTutorial
		);

		startCoroutines(
			_sweetenerCoroutine,
			_swellCoroutine
		);

		_swabbieCoroutine = cycleTracksFrequenecyRange(
			_swabbie,
			ShortestSweetenerPlayFrequenecy,
			LongestSweetenerPlayFrequenecy
		);
        /*
		if ((PSScene)Application.loadedLevel == PSScene.MainGame) {
			startCoroutines(_ambienceMainCoroutine,_swabbieCoroutine);
		}
        */
		if ((PSScene)Application.loadedLevel == PSScene.TutorialScene) {
			startCoroutines(_ambienceTutorialCoroutine);
		}
	}

	public void StopTrackCycling () {
		StopCoroutine(_sweetenerCoroutine);
		StopCoroutine(_swellCoroutine);
		StopCoroutine(_ambienceMainCoroutine);
		StopCoroutine(_ambienceTutorialCoroutine);
		StopCoroutine(_swabbieCoroutine);
	}

	public void ClickSound () {
		Play (_GUIclicks.Cycle ());
	}

	public void Matey () {
		Play (_matey.Cycle ());
	}

	public void VoiceEffect (string name) {
		switch (name) {
		case "FirstMateSpeech":
			Play (_firstMate.Cycle ());
			break;
		case "QuartermasterSpeech":
			Play (_quarterMaster.Cycle ());
			break;
		case "SwabbieSpeech":
			Play (_swabbieSpeech.Cycle ());
			break;
		case "RiggerSpeech":
			Play (_rigger.Cycle ());
			break;
		case "SecondMateSpeech":
			Play (_secondMate.Cycle ());
			break;
		case "OJSpeech":
			Play (_oj.Cycle ());
			break;
		case "SadieAhoy":
			Play (_sadieAhoy.Cycle ());
			break;
		case "SadieTalk":
			Play (_sadieTalk.Cycle ());
			break;
		case "ShipmasterSpeech":
			Play (_shipmaster.Cycle ());
			break;
		default:
			break;
		}
	}

	public void SwabbieRun () {
		StopCoroutine (_swabbieCoroutine);
	}

	void initCyclingAudio () {
		_sweeteners = new RandomizedQueue<AudioFile>();
		_swells = new RandomizedQueue<AudioFile>();
		_GUIclicks = new RandomizedQueue<AudioFile>();
		_matey = new RandomizedQueue<AudioFile>();
		_ambienceMain = new RandomizedQueue<AudioFile>();
		_ambienceTutorial = new RandomizedQueue<AudioFile>();
		_swabbie = new RandomizedQueue<AudioFile>();
		_firstMate = new RandomizedQueue<AudioFile>();
		_swabbieSpeech = new RandomizedQueue<AudioFile>();
		_quarterMaster = new RandomizedQueue<AudioFile>();
		_rigger = new RandomizedQueue<AudioFile>();
		_secondMate = new RandomizedQueue<AudioFile>();
		_oj = new RandomizedQueue<AudioFile>();
		_sadieAhoy = new RandomizedQueue<AudioFile>();
		_sadieTalk = new RandomizedQueue<AudioFile>();
		_shipmaster = new RandomizedQueue<AudioFile>();
		// Init Queue's with sound files
		List<AudioFile> list = new List<AudioFile>();
		// Get all deck music
		playEvents.TryGetValue ("onesoundtrackevery100to200seconds",out list);
		foreach (AudioFile track in list) {
			_swells.Enqueue (track);
		}
		playEvents.TryGetValue ("every8to20seconds",out list);
		foreach (AudioFile track in list) {
			_sweeteners.Enqueue (track);
		}
		// Get all GUI click sounds
		playEvents.TryGetValue ("GUI Click",out list);
		foreach (AudioFile track in list) {
			_GUIclicks.Enqueue (track);
		}
		// Get all matey sounds
		playEvents.TryGetValue ("MateyButton",out list);
		foreach (AudioFile track in list) {
			_matey.Enqueue (track);
		}
		// Get ambience
		playEvents.TryGetValue ("EnterTutorial",out list);
		foreach (AudioFile track in list) {
			_ambienceTutorial.Enqueue (track);
		}
		playEvents.TryGetValue ("enterscene",out list);
		foreach (AudioFile track in list) {
			_ambienceMain.Enqueue (track);
		}
		// Get swabbie mopping sound
		playEvents.TryGetValue ("MopOnFloor",out list);
		foreach (AudioFile track in list) {
			_swabbie.Enqueue (track);
		}
		// Speaking voice effects
		playEvents.TryGetValue ("FirstMateSpeech",out list);
		foreach (AudioFile track in list) {
			_firstMate.Enqueue (track);
		}

		playEvents.TryGetValue ("SwabbieTalk",out list);
		foreach (AudioFile track in list) {
			_swabbieSpeech.Enqueue (track);
		}

		playEvents.TryGetValue ("RiggerTalk",out list);
		foreach (AudioFile track in list) {
			_rigger.Enqueue (track);
		}

		playEvents.TryGetValue ("QuartermasterTalk",out list);
		foreach (AudioFile track in list) {
			_quarterMaster.Enqueue (track);
		}

		playEvents.TryGetValue ("SecondmateTalks",out list);
		foreach (AudioFile track in list) {
			_secondMate.Enqueue (track);
		}

		playEvents.TryGetValue ("OJTalk",out list);
		foreach (AudioFile track in list) {
			_oj.Enqueue (track);
		}

		playEvents.TryGetValue ("SadieAhoy",out list);
		foreach (AudioFile track in list) {
			_sadieAhoy.Enqueue (track);
		}

		playEvents.TryGetValue ("SadieTalk",out list);
		foreach (AudioFile track in list) {
			_sadieTalk.Enqueue (track);
		}

		playEvents.TryGetValue ("ShipmasterTalk",out list);
		foreach (AudioFile track in list) {
			_shipmaster.Enqueue (track);
		}

	}

	// Plays audio files back to back
	IEnumerator cycleTracksContinuous (RandomizedQueue<AudioFile> files) {
		while (_coroutinesActive) {	
			AudioFile nextTrack = files.Cycle();
			Play(nextTrack);
			yield return new WaitForSeconds(nextTrack.Clip.length);
		}
	}

	// Plays audio files on a set frequenecy
	IEnumerator cycleTracksFrequenecy (RandomizedQueue<AudioFile> files, float frequenecy) {
		while (_coroutinesActive) {
			Play(files.Cycle());
			yield return new WaitForSeconds(frequenecy);
		}
	}

	// Coroutine that varies the frequency with which it plays audio files
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

	// Starts an arbitrary amount of coroutines
	void startCoroutines (params IEnumerator[] coroutines) {
		for (int i = 0; i < coroutines.Length; i++) {
			StartCoroutine(coroutines[i]);
		}
	}

}
