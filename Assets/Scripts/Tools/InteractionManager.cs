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
	public string _name { private get; set; }
	public string iName { get { return (_name == null) ? string.Empty : _name; } }

	[XmlElement("Type")]
	public string _type { private get; set; }
	public InteractionType iType { 
		get {
			bool realType = _type != null && Enum.IsDefined (typeof(InteractionType), _type);
			return realType ? (InteractionType)Enum.Parse (typeof(InteractionType), _type) : InteractionType.Click;
		}
	}
		
	[XmlElement("ALLTags")]
	public string _allTags { private get; set;}
	public List<string> iAllTags { 
		get { 
			bool noTags = _allTags == null || _allTags == string.Empty;
			return noTags ? new List<string> () : _allTags.Split ('|').ToList ();
		}
	}

	[XmlElement("ANYTags")]
	public string _anyTags { private get; set;}
	public List<string> iAnyTags { 
		get { 
			bool noTags = _anyTags == null || _anyTags == string.Empty;
			return noTags ? new List<string> () : _anyTags.Split ('|').ToList ();
		}
	}

	[XmlElement("NONETags")]
	public string _noneTags { private get; set;}
	public List<string> iNoneTags { 
		get { 
			bool noTags = _noneTags == null || _noneTags == string.Empty;
			return noTags ? new List<string> () : _noneTags.Split ('|').ToList ();
		}
	}

	[XmlElement("MaxDistance")]
	public string _maxDist { private get; set; }
	public float iMaxDist { 
		get {
			float md = 1000000f;
			bool valid = _maxDist != null && float.TryParse (_maxDist, out md); 
			return valid ? md : 1000000f;
		}
	}

	[XmlElement("TooFar")]
	public string _tooFar { private get; set; }
	public string iTooFar { get { return (_tooFar == null) ? "DefaultTooFar" : _tooFar; } }

	[XmlElement("Text")]
	public string _text { private get; set; }
	public bool HasText { get { return _text != null && _text != string.Empty; } }
	public string iText { get { return HasText ? _text : null; } }

	[XmlElement("Image")]
	public string _image { private get; set; }
	public bool HasImage { get { return _image != null && iImage != null; } }
	public Sprite iImage {
		//*
		get {
			Texture2D image = Resources.Load<Texture2D> (string.Concat ("Visual/", _image));
			if (image != null) {
				Rect rect = new Rect (0, 0, image.width, image.height);
				Vector2 pivot = Vector2.zero;
				return Sprite.Create (image, rect, pivot);
			} else {
				return null;
			}
		}/*/
		get{ 
			Sprite sprite = Resources.Load<Sprite> ("Visual/" + _image);
			return (sprite == null) ? null : sprite;
		}/**/
	}

	[XmlElement("Next")]
	public string _next { private get; set; }
	public bool HasNext { get { return _next != null && _next != string.Empty; } }
	public string iNext { get { return HasNext ? _next : null; } }

	[XmlElement("GiveTags")]
	public string _giveTags { private get; set;}
	public List<string> iGiveTags { 
		get { 
			bool noTags = _giveTags == null || _giveTags == string.Empty;
			return noTags ? new List<string> () : _giveTags.Split ('|').ToList ();
		}
	}

	[XmlElement("TakeTags")]
	public string _takeTags { private get; set;}
	public List<string> iTakeTags { 
		get { 
			bool noTags = _takeTags == null || _takeTags == string.Empty;
			return noTags ? new List<string> () : _takeTags.Split ('|').ToList ();
		}
	}

	[XmlElement("GiveItems")]
	public string _giveItems { private get; set;}
	public List<string> iGiveItems { 
		get { 
			bool noTags = _giveItems == null || _giveItems == string.Empty;
			return noTags ? new List<string> () : _giveItems.Split ('|').ToList ();
		}
	}

	[XmlElement("TakeItems")]
	public string _takeItems { private get; set;}
	public List<string> iTakeItems { 
		get { 
			bool noTags = _takeItems == null || _takeItems == string.Empty;
			return noTags ? new List<string> () : _takeItems.Split ('|').ToList ();
		}
	}

	[XmlElement("SpecialActions")]
	public string _specialActions { private get; set;}
	public List<string> iSpecialActions { 
		get { 
			bool noTags = _specialActions == null || _specialActions == string.Empty;
			return noTags ? new List<string> () : _specialActions.Split ('|').ToList ();
		}
	}

	public bool IsValid {
		get {
			bool hasALL = TagManager.Instance.HasAllTags (iAllTags);
			bool hasANY = TagManager.Instance.HasAnyTags (iAnyTags);
			bool hasNONE = TagManager.Instance.HasNoneTags (iNoneTags);
			if (GameManager.DEBUGGING) {
				Debug.Log ("Checking Validity for " + iName + ". ALL: " + hasALL.ToString() + " ANY: " + hasANY.ToString() + " NONE: " + hasNONE.ToString() + " . Returning " + (hasALL && hasANY && hasNONE).ToString());
			}
			return (hasALL && hasANY && hasNONE);
		}
	}

	override public string ToString(){
		return "Name: " + iName +
		" Type: " + iType +
		" ALLTags: " + string.Join (" ", iAllTags.ToArray ()) +
		" ANYTags: " + string.Join (" ", iAnyTags.ToArray ()) +
		" NONETags: " + string.Join (" ", iNoneTags.ToArray ()) +
		" MaxDist: " + iMaxDist.ToString () +
		" HasText: " + HasText +
		" HasImage " + HasImage;
	}
}

[XmlRoot("InteractionList")]
public class InteractionList{
	[XmlArray("Interactions")]
	[XmlArrayItem("Interaction")]

	public List<Interaction> List = new List<Interaction>();

	public static List<Interaction> Load(string path){
		TextAsset _xml = Resources.Load<TextAsset> (path);

		if (_xml == null) {
			return new List<Interaction> ();
		}

		XmlSerializer serializer = new XmlSerializer (typeof(InteractionList));

		StringReader reader = new StringReader (_xml.text);

		InteractionList iList = serializer.Deserialize (reader) as InteractionList;

		reader.Close ();
	
		return iList.List;
	}
}

public class InteractionManager{
	/*currently a utility class. all methods must be static.
	private InteractionManager _instance;
	public InteractionManager Instance {
		get {
			if (_instance == null) {
				_instance = new InteractionManager ();
			}
			return _instance;
		}
	}*/

	public static void HandleInteractionList(Interactable interactor, List<Interaction> possibleInteractions){
		List<Interaction> validInteractions = possibleInteractions.FindAll (interaction => interaction.IsValid);
		if (GameManager.DEBUGGING) {
			Debug.Log ("Possible Interactions: " + possibleInteractions.Count.ToString());
			Debug.Log ("Valid Interactions: " + string.Join (" ", validInteractions.ConvertAll (new Converter<Interaction, string> (i => i.iName)).ToArray ()));
		}
		float interactionDistance = Vector3.Distance (interactor.transform.position, GameManager.Instance.playerCharacter.transform.position);
		//UIManager.Instance.CloseInteractionPanel ();
		//if all of the otherwise valid interactions are close enough to happen, do them all.
		if (validInteractions.TrueForAll (interaction => interactionDistance <= interaction.iMaxDist)) {
			foreach (Interaction interaction in validInteractions) {
				HandleInteractionSingle (interactor, interaction);
			}
		} else {
			//if some of them aren't close enough, find their alternatives, and handle a new list of the alternatives and the interactions that were close enough.
			List<Interaction> tooFars = validInteractions.FindAll (i => interactionDistance > i.iMaxDist);
			List<Interaction> closeEnoughs = validInteractions.FindAll (i => interactionDistance <= i.iMaxDist);
			List<Interaction> alternatives = new List<Interaction> ();
			foreach (Interaction interaction in tooFars) {
				alternatives = alternatives.Union (interactor.Interactions.FindAll (i => (i.iName == interaction.iTooFar) && (i.iType == InteractionType.Derivative))).ToList ();
			}
			alternatives = alternatives.Union (closeEnoughs).ToList ();
			HandleInteractionList (interactor, alternatives);
		}
	}

	public static void HandleInteractionSingle(Interactable interactor, Interaction interactionToHandle){
		//if the interaction has text, display it as a button. If the player chooses the button, the rest of the interaction will get processed by InteractionButtonPressed
		if (interactionToHandle.HasText) {
			//UIManager.Instance.ShowInteractionPanel ();
			InteractionButton.Generate (interactor, interactionToHandle);
			if (interactionToHandle.HasImage) {
				UIManager.Instance.ChangeInteractionImage (interactionToHandle.iImage);
			}
		} else {
		//if the interaction doesn't have text, just process the rest of the interaction.
			TagManager.Instance.TakeTags (interactionToHandle.iTakeTags);
			TagManager.Instance.GiveTags (interactionToHandle.iGiveTags);
			InventoryManager.Instance.TakeItems (interactionToHandle.iTakeItems);
			InventoryManager.Instance.GiveItems (interactionToHandle.iGiveItems);
			interactor.DoSpecialActions (interactionToHandle.iSpecialActions);
		}
	}

	public static void InteractionButtonPressed(Interactable interactor, Interaction interaction){
		//close the interaction panel. If the next of this interaction has text, it will open another one in HandleInteractionSingle
		UIManager.Instance.CloseInteractionPanel ();
		//process the rest of the interaction. This part was put on hold because the interaction had text, and could be part of a choice menu.
		TagManager.Instance.TakeTags (interaction.iTakeTags);
		TagManager.Instance.GiveTags (interaction.iGiveTags);
		InventoryManager.Instance.TakeItems (interaction.iTakeItems);
		InventoryManager.Instance.GiveItems (interaction.iGiveItems);
		interactor.DoSpecialActions (interaction.iSpecialActions);
		//if the interaction has followup interactions, find all of them and handle the list.
		if (interaction.HasNext) {
			List<Interaction> nextInteractions = interactor.Interactions.FindAll (i => (i.iName == interaction.iNext) && (i.iType == InteractionType.Derivative));
			HandleInteractionList (interactor, nextInteractions);
		}
	}
}
