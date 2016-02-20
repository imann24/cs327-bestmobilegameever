using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;

public enum InteractionType{
	Click, 
	Orange,
	UseItem,
	Derivative}

public class Interaction {
	[XmlElement("Name")]
	public string iName;

	[XmlElement("Type")]
	public string _type { private get; set; }
	public InteractionType iType {
		get {
			if (Enum.IsDefined (typeof(InteractionType), _type)) {
				return (InteractionType)Enum.Parse (typeof(InteractionType), _type);
			} else {
				if (GameManager.DEBUGGING) {
					Debug.Log (_type + " is not a valid InteractionType.");
				}
				return InteractionType.Click;
			}
		}
	}
		
	[XmlElement("ALLTags")]
	public string _allTags { private get; set;}
	public List<string> iAllTags { get { return (_allTags.Length == 0) ? new List<string>() : _allTags.Split ('|').ToList (); } }

	[XmlElement("ANYTags")]
	public string _anyTags { private get; set;}
	public List<string> iAnyTags { get { return (_anyTags.Length == 0) ? new List<string>() : _anyTags.Split ('|').ToList (); } }

	[XmlElement("NONETags")]
	public string _noneTags { private get; set;}
	public List<string> iNoneTags { get { return (_noneTags.Length == 0) ? new List<string>() : _noneTags.Split ('|').ToList (); } }

	[XmlElement("Text")]
	public string iText;
	public bool HasText { get { return iText != null; } }

	[XmlElement("Image")]
	public string _image { private get; set; }
	public bool HasImage { get { return iImage != null; } }
	public Sprite iImage {
		get {
			Texture2D image = Resources.Load<Texture2D> (string.Concat ("Visual/", _image));
			if (image != null) {
				Rect rect = new Rect (0, 0, image.width, image.height);
				Vector2 pivot = Vector2.zero;
				return Sprite.Create (image, rect, pivot);
			} else {
				return null;
			}
		}
	}

	[XmlElement("Next")]
	public string iNext;

	[XmlElement("GiveTags")]
	public string _giveTags { private get; set;}
	public List<string> iGiveTags { get { return (_giveTags == "") ? new List<string>() : _giveTags.Split ('|').ToList (); } }

	[XmlElement("TakeTags")]
	public string _takeTags { private get; set;}
	public List<string> iTakeTags { get { return (_takeTags == "") ? new List<string>() : _takeTags.Split ('|').ToList (); } }

	[XmlElement("GiveItems")]
	public string _giveItems { private get; set;}
	public List<string> iGiveItems { get { return (_giveItems == "") ? new List<string>() : _giveItems.Split ('|').ToList (); } }

	[XmlElement("TakeItems")]
	public string _takeItems { private get; set;}
	public List<string> iTakeItems { get { return (_takeItems == "") ? new List<string>() : _takeItems.Split ('|').ToList (); } }

	[XmlElement("SpecialActions")]
	public string _specialActions { private get; set;}
	public List<string> iSpecialActions { get { return (_specialActions == "") ? new List<string>() : _specialActions.Split ('|').ToList (); } }

	public bool IsValid {
		get {
			bool hasALL = TagManager.Instance.HasAllTags (iAllTags);
			bool hasANY = TagManager.Instance.HasAnyTags (iAnyTags);
			bool hasNONE = TagManager.Instance.HasNoneTags (iNoneTags);
			if (GameManager.DEBUGGING) {
				Debug.Log ("Checking Validity. ALL: " + hasALL.ToString() + " ANY: " + hasANY.ToString() + " NONE: " + hasNONE.ToString());
			}
			return (hasALL && hasANY && hasNONE);
		}
	}

	override public string ToString(){
		return "Name: " + iName + " Type: " + iType.ToString ();// + " AnyTags: " + string.Join (" ", iAnyTags.ToArray ());
	}
}

[XmlRoot("InteractionList")]
public class InteractionList{
	[XmlArray("Interactions")]
	[XmlArrayItem("Interaction")]

	public List<Interaction> List = new List<Interaction>();

	public static List<Interaction> Load(string path){
		TextAsset _xml = Resources.Load<TextAsset> (path);

		XmlSerializer serializer = new XmlSerializer (typeof(InteractionList));

		StringReader reader = new StringReader (_xml.text);

		InteractionList iList = serializer.Deserialize (reader) as InteractionList;

		reader.Close ();
	
		return iList.List;
	}
}

public class InteractionManager{
	//currently singleton. May remove if all methods end up static and this becomes a utility class.
	private InteractionManager _instance;
	public InteractionManager Instance {
		get {
			if (_instance == null) {
				_instance = new InteractionManager ();
			}
			return _instance;
		}
	}

	public static void HandleInteractions(Interactable interactor, List<Interaction> possibleInteractions){
		List<Interaction> validInteractions = possibleInteractions.FindAll (interaction => interaction.IsValid);
		if (GameManager.DEBUGGING) {
			Debug.Log ("Searching for valid interactions. Number of valid interactions:" + validInteractions.Count);
		}
		foreach (Interaction interaction in validInteractions) {
			if (interaction.HasText) {
				UIManager.Instance.ShowInteractionPanel ();
				InteractionButton.Generate (interactor, interaction, possibleInteractions);
				if (interaction.HasImage) {
					UIManager.Instance.ChangeInteractionImage (interaction.iImage);
				}
			} else {
				TagManager.Instance.TakeTags (interaction.iTakeTags);
				TagManager.Instance.GiveTags (interaction.iGiveTags);
				InventoryManager.Instance.TakeItems (interaction.iTakeItems);
				InventoryManager.Instance.GiveItems (interaction.iGiveItems);
				interactor.DoSpecialActions (interaction.iSpecialActions);
			}
		}
	}

	public static void CompleteInteraction(Interactable completer, Interaction completed){
		UIManager.Instance.CloseInteractionPanel ();
		TagManager.Instance.TakeTags (completed.iTakeTags);
		TagManager.Instance.GiveTags (completed.iGiveTags);
		InventoryManager.Instance.TakeItems (completed.iTakeItems);
		InventoryManager.Instance.GiveItems (completed.iGiveItems);
		completer.DoSpecialActions (completed.iSpecialActions);
		if (completed.iNext != "") {
			List<Interaction> nameMatches = completer.Interactions.FindAll (interaction => (interaction.iName == completed.iNext) &&
			                                (interaction.iType == InteractionType.Derivative));
			if (GameManager.DEBUGGING) {
				Debug.Log ("Checking next interactions. Possible Next Count: " + completer.Interactions.Count + " Matches: " + nameMatches.Count);
			}
			HandleInteractions (completer, nameMatches);
		}
	}
}
