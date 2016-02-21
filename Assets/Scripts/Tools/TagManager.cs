using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TagManager{
	//This class maintains the player progression via a list of strings, or tags. It can add, remove, and check for tags and lists of tags.

	private static TagManager _instance;
	public static TagManager Instance {
		get {
			if (_instance == null) {
				_instance = new TagManager ();
			}
			return _instance;
		}
	}

	public List<string> PlayerTags { get; private set; }

	private TagManager(){
		PlayerTags = new List<string> ();
		PlayerTags.Add ("intro");
	}

	public void Load(List<string> tags){
		PlayerTags = tags;
	}

	public void GiveTag(string tag){
		PlayerTags.Add (tag);
		PlayerTags.TrimExcess ();
	}

	public void GiveTags(List<string> tags){
		foreach (string tag in tags) {
			PlayerTags.Add (tag);
		}
		PlayerTags.TrimExcess ();
	}

	public void TakeTag(string tag){
		PlayerTags.Remove (tag);
	}

	public void TakeTags(List<string> tags){
		foreach (string tag in tags) {
			PlayerTags.Remove (tag);
		}
	}

	public bool HasTag(string tag){
		if (GameManager.DEBUGGING) {
			Debug.Log ("Checking tag: " + tag + "Player tags: " + string.Join(" ", PlayerTags.ToArray()));
		}
		return PlayerTags.Contains (tag);
	}

	public bool HasAllTags(List<string> tags){
		if (tags.Count > 0) {
			return tags.TrueForAll (tag => HasTag (tag));
		} else {
			return true;
		}
	}

	public bool HasAnyTags(List<string> tags){
		if (tags.Count > 0) {
			return tags.FindAll (tag => HasTag (tag)).Count > 0;
		} else {
			return true;
		}
	}

	public bool HasNoneTags(List<string> tags){
		if (tags.Count > 0) {
			List<string> conflictingTags = tags.FindAll(tag => HasTag(tag));
			if (GameManager.DEBUGGING) {
				Debug.Log ("Checking HasNoneTags. Conflicting tag count: " + conflictingTags.Count);
			}
			return conflictingTags.Count == 0;
		} else {
			return true;
		}
	}
}
