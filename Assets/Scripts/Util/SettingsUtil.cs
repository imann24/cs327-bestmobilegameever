/*
* Author: Isaiah Mann
* Description: Util class for simple player settings
*/
using UnityEngine;
using System.Collections;

public static class SettingsUtil {
	
	// Keys used to acccess the settings from player prefs
	const string musicMuteSettingsKey = "musicMute";
	const string fxMuteSettingsKey = "fxMute";

	public static void ToggleMusicMuted (bool muted) {
		ToggleMute (
			musicMuteSettingsKey,
			muted
		);

		EventController.Event (
			AudioUtil.MuteActionFromBool(muted),
			AudioType.Music
		);
	}

	public static void ToggleFXMuted (bool muted) {
		ToggleMute (
			fxMuteSettingsKey,
			muted
		);

		EventController.Event (
			AudioUtil.MuteActionFromBool(muted),
			AudioType.FX
		);
	}

	public static bool musicMuted {
		get {
			return IsMuted(musicMuteSettingsKey);
		}
	}

	public static bool fxMuted {
		get {
			return IsMuted(fxMuteSettingsKey);
		}
	}

	static void ToggleMute (string key, bool value) {
		PlayerPrefsUtil.SetBool(
			key,
			value
		);
	}

	static bool IsMuted (string key) {
		return PlayerPrefsUtil.GetBool(key);
	}
}