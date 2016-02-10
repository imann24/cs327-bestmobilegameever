/*
 * Author: Isaiah Mann
 * Description: Class to load in the audio from a JSON file
 * Dependencies: None
 */
using UnityEngine;
using System.Collections;

public class AudioLoader {
	const string DIRECTORY = "Audio/";
	string _path;

	public AudioLoader (string path) {
		this._path = path;
	}

	public AudioList Load () {
		return JsonUtility.FromJson<AudioList>(
			FileUtil.FileText (
				this._path
			)
		);
	}

	public static AudioClip GetClip (string fileName) {
		return Resources.Load<AudioClip>(
			DIRECTORY + fileName
		);
	}

	public static AudioClip GetClip (AudioFile file) {
		return GetClip(file.FileName);
	}

}