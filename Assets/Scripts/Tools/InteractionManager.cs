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
	DropItem,
	Derivative}

public enum TextType{
	Option,
	Floating,
	Monologue
}

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

	[XmlElement("TextType")]
	public string _textType { private get; set; }
	public TextType iTextType { 
		get {
			bool realType = _textType != null && Enum.IsDefined (typeof(TextType), _textType);
			return realType ? (TextType)Enum.Parse (typeof(TextType), _textType) : TextType.Option;
		}
	}

	[XmlElement("Image")]
	public string _image { private get; set; }
	public bool HasImage { get { return _image != null && iImage != null; } }
	public Sprite iImage {
		get {
			Texture2D image = Resources.Load<Texture2D> (string.Concat ("Visual/", _image));
			if (image != null) {
				Rect rect = new Rect (0, 0, image.width, image.height);
				Vector2 pivot = Vector2.zero;
				return Sprite.Create (image, rect, pivot);
			} else {
				if (GameManager.DEBUGGING) {
					Debug.Log ("Could not find a texture named " + _image + ". Check the Resources/Visual folder.");
				}
				return null;
			}
		}
	}

	[XmlElement("Image2")]
	public string _image2 { private get; set; }
	public bool HasImage2 { get { return _image != null && iImage != null; } }
	public Sprite iImage2 {
		get {
			Texture2D image = Resources.Load<Texture2D> (string.Concat ("Visual/", _image2));
			if (image != null) {
				Rect rect = new Rect (0, 0, image.width, image.height);
				Vector2 pivot = Vector2.zero;
				return Sprite.Create (image, rect, pivot);
			} else {
				if (GameManager.DEBUGGING) {
					Debug.Log ("Could not find a texture named " + _image2 + ". Check the Resources/Visual folder.");
				}
				return null;
			}
		}
	}

	[XmlElement("Next")]
	public string _next { private get; set; }
	public bool HasNext { get { return _next != null && _next != string.Empty; } }
	public string iNext { get { return HasNext ? _next : null; } }

	[XmlElement("NextInteractor")]
	public string _nextInteractor { private get; set; }
	public bool NextNotSelf { get { return _nextInteractor != null && _nextInteractor != string.Empty; } }
	public string iNextInteractor { get { return NextNotSelf ? _nextInteractor : null; } }

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

/// <summary>
/// This utility class is responsible for executing all
/// </summary>
public class InteractionManager{


	public static void HandleOnClick(Interactable interactor){
		List<Interaction> clickInteractions = interactor.Interactions.FindAll (i => i.iType == InteractionType.Click);
		if (interactor.Debugging) {
			Debug.Log ("Clicked on " + interactor.gameObject.name + ". Possible interactions: " + string.Join (" ", clickInteractions.ConvertAll (new Converter<Interaction, string> (i => i.iName)).ToArray ()));
		}
		HandleInteractionList (interactor, clickInteractions);
	}

	public static void HandleOnOrange(Interactable interactor){
		List<Interaction> orangeInteractions = interactor.Interactions.FindAll (i => i.iType == InteractionType.Orange);
		if (interactor.Debugging) {
			Debug.Log ("Hit " + interactor.gameObject.name +" with an orange. Possible interactions: " + string.Join (" ", orangeInteractions.ConvertAll (new Converter<Interaction, string> (i => i.iName)).ToArray ()));
		}
		HandleInteractionList (interactor, orangeInteractions);
	}
		
	public static void HandleUseItem(Interactable interactor){
		List<Interaction> itemInteractions = interactor.Interactions.FindAll (i => i.iType == InteractionType.UseItem);//all interactions of type useItem
		List<Interaction> prevalidatedItemInteractions = itemInteractions.FindAll (i => i.IsValid);//all VALID interactions of type useItem
		Interactable selected = InventoryManager.Instance.selectedItem.GetComponent<Interactable> ();//the selected item's interactable component
		if (interactor.Debugging) {
			Debug.Log ("Using " + selected.gameObject.name + " on " + interactor.gameObject.name + ". Possible interactions: " + string.Join (" ", prevalidatedItemInteractions.ConvertAll (new Converter<Interaction, string> (i => i.iName)).ToArray ()));
		}
		if (prevalidatedItemInteractions.Count > 0) {//if there is at least one valid interaction, handle them.
			HandleInteractionList (interactor, prevalidatedItemInteractions);
		} else {//otherwise try to get the appropriate default cannot use
			if (interactor.Debugging) {
				Debug.Log ("No interactions were found. Check the tags for " + interactor.gameObject.name + "'s UseItem interactions. The active PlayerTags were: " + string.Join (" ", TagManager.Instance.PlayerTags.ToArray ()));
			}
			if (selected != null) {
				Interaction error = selected.Interactions.Find (i => i.iName == "DefaultCannotUse");
				if (error != null) {
					HandleInteractionSingle (selected, error);
				} else if (interactor.Debugging) {
					Debug.Log ("The selected item has not DefaultCannotUse interaction.");
				}
			} else if (interactor.Debugging) {
				Debug.Log ("The selected object has no interactable component.");
			}
		}
	}

	public static void HandleOnDrop(Interactable interactor){
		List<Interaction> dropInteractions = interactor.Interactions.FindAll (i => i.iType == InteractionType.DropItem);
		if (interactor.Debugging) {
			Debug.Log ("Dropped " + interactor.gameObject.name +". Possible interactions: " + string.Join (" ", dropInteractions.ConvertAll (new Converter<Interaction, string> (i => i.iName)).ToArray ()));
		}
		HandleInteractionList (interactor, dropInteractions);
	}


	/// <summary>
	/// Validate the given list of interactions using the player's tags. Then check if the interactions are all close enough to occur.
	/// If they are, handle them. If not, find alternatives for the interactions that are too far away, and handle a new list of the alternatives
	/// and the interactions that WERE close enough.
	/// </summary>
	/// <param name="interactor">Interactor.</param>
	/// <param name="possibleInteractions">Possible interactions.</param>
	public static void HandleInteractionList(Interactable interactor, List<Interaction> possibleInteractions){
		List<Interaction> validInteractions = possibleInteractions.FindAll (interaction => interaction.IsValid);
		if (GameManager.DEBUGGING) {
			Debug.Log ("Possible Interactions: " + possibleInteractions.Count.ToString());
			Debug.Log ("Valid Interactions: " + string.Join (" ", validInteractions.ConvertAll (new Converter<Interaction, string> (i => i.iName)).ToArray ()));
		}
		float interactionDistance = Vector3.Distance (interactor.transform.position, GameManager.Instance.playerCharacter.transform.position);

		if (validInteractions.TrueForAll (interaction => interactionDistance <= interaction.iMaxDist)) {
			int numTexts = validInteractions.FindAll (i => i.HasText && i.iTextType != TextType.Floating).Count;
			foreach (Interaction interaction in validInteractions) {
				if (interaction.HasText) {
					DisplayInteraction (interactor, interaction, numTexts);
				} else {
					CompleteInteraction (interactor, interaction);
				}
			}
		} else {
			//Here is where we find the alternatives and recall the function.
			List<Interaction> tooFars = validInteractions.FindAll (i => interactionDistance > i.iMaxDist);
			List<Interaction> closeEnoughs = validInteractions.FindAll (i => interactionDistance <= i.iMaxDist);
			List<Interaction> alternatives = new List<Interaction> ();
			foreach (Interaction interaction in tooFars) {
				//add all interactions in interactor's interactionList that match the tooFar name.
				alternatives = alternatives.Union (interactor.Interactions.FindAll (i => i.iName == interaction.iTooFar)).ToList ();
			}
			alternatives = alternatives.Union (closeEnoughs).ToList ();
			HandleInteractionList (interactor, alternatives);
		}
	}

	/// <summary>
	/// Forces the handling of a single interaction. Does not check if the interaction is valid, too far, or otherwise inappropriate. Use carefully.
	/// </summary>
	/// <param name="interactor">Interactor.</param>
	/// <param name="interactionToHandle">Interaction to handle.</param>
	public static void HandleInteractionSingle(Interactable interactor, Interaction interactionToHandle){
		if (interactionToHandle.HasText) {
			DisplayInteraction (interactor, interactionToHandle, 1);
		} else {
			CompleteInteraction (interactor, interactionToHandle);
		}
	}

	/// <summary>
	/// Handles displaying the interaction, depending on tags in the interaction xml document.
	/// </summary>
	/// <param name="interactor">The interactor that will speak if the Floating TextType is present.</param>
	/// <param name="interaction">The interaction being displayed.</param>
	/// <param name="numberOfTexts">The total text interactions that will be displayed.</param> 
	static void DisplayInteraction(Interactable interactor, Interaction interaction, int numberOfTexts){
		if (interaction.HasText) {
			switch (interaction.iTextType) {
			case TextType.Floating:
				interactor.transform.GetComponentInChildren<SpeechBubble> ().Say (interaction.iText);
				break;
			case TextType.Monologue:
				UIManager.Instance.GenerateMonologue (interactor, interaction);
				if (interaction.HasImage) {
					UIManager.Instance.ShowInteractionImageLeft (interaction.iImage);
				}
				if (interaction.HasImage2) {
					UIManager.Instance.ShowInteractionImageRight (interaction.iImage2);
				}
				if (numberOfTexts == 1) {
					UIManager.Instance.EnableTapToContinue (interactor, interaction);
				}
				break;
			case TextType.Option:
				UIManager.Instance.GenerateOption (interactor, interaction);
				if (interaction.HasImage) {
					UIManager.Instance.ShowInteractionImageLeft (interaction.iImage);
				}
				if (interaction.HasImage2) {
					UIManager.Instance.ShowInteractionImageRight (interaction.iImage2);
				}
				if (numberOfTexts == 1) {
					UIManager.Instance.EnableTapToContinue (interactor, interaction);
				}
				break;
			}
		}
	}

	/// <summary>
	/// Completes the interaction. Give/Take Items and Tags, execute any special actions, then locate followup interactions if necessary and begin those.
	/// </summary>
	/// <param name="interactor">The interactor executing special actions.</param>
	/// <param name="interaction">The interaction being completed.</param>
	static void CompleteInteraction(Interactable interactor, Interaction interaction){
		TagManager.Instance.TakeTags (interaction.iTakeTags);
		TagManager.Instance.GiveTags (interaction.iGiveTags);
		InventoryManager.Instance.TakeItems (interaction.iTakeItems);
		InventoryManager.Instance.GiveItems (interaction.iGiveItems);
		interactor.DoSpecialActions (interaction.iSpecialActions);
		//if the interaction has followups, figure out who they belong to and execute them.
		if (interaction.HasNext) {
			Interactable nextInteractor = interaction.NextNotSelf ? GameObject.Find (interaction.iNextInteractor).GetComponent<Interactable> () : interactor;
			List<Interaction> nextInteractions = nextInteractor.Interactions.FindAll (i => (i.iName == interaction.iNext) && (i.iType == InteractionType.Derivative));
			HandleInteractionList (nextInteractor, nextInteractions);
		}
	}

	public static void InteractionButtonPressed(Interactable interactor, Interaction interaction){
		//close the interaction panel. If the next of this interaction has text, it will open another one in HandleInteractionSingle
		UIManager.Instance.CloseInteractionPanel ();
		CompleteInteraction(interactor, interaction);
	}
}
