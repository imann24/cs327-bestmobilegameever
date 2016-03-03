using UnityEngine;
using UnityEngine.UI;
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
				#if (DEBUG)
					Debug.Log ("Could not find a texture named " + _image + ". Check the Resources/Visual folder.");
				#endif
				return null;
			}
		}
	}

	[XmlElement("Image2")]
	public string _image2 { private get; set; }
	public bool HasImage2 { get { return _image2 != null && iImage2 != null; } }
	public Sprite iImage2 {
		get {
			Texture2D image = Resources.Load<Texture2D> (string.Concat ("Visual/", _image2));
			if (image != null) {
				Rect rect = new Rect (0, 0, image.width, image.height);
				Vector2 pivot = Vector2.zero;
				return Sprite.Create (image, rect, pivot);
			} else {
				#if (DEBUG)
				Debug.Log ("Could not find a texture named " + _image2 + ". Check the Resources/Visual folder.");
				#endif
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
			bool hasALL = GameManager.HasAllTags (iAllTags);
			bool hasANY = GameManager.HasAnyTags (iAnyTags);
			bool hasNONE = GameManager.HasNoneTags (iNoneTags);
			#if (DEBUG)
				Debug.Log ("Checking Validity for " + iName + ". ALL: " + hasALL.ToString() + " ANY: " + hasANY.ToString() + " NONE: " + hasNONE.ToString() + " -- Returning " + (hasALL && hasANY && hasNONE).ToString());
			#endif
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

public class InteractionManager : MonoBehaviour {

	[SerializeField]
	private GameObject monologueDisplay = null;
	[SerializeField]
	private GameObject optionDisplay = null;
	[SerializeField]
	private GameObject textPanel = null;
	[SerializeField]
	private GameObject leftImage = null;
	[SerializeField]
	private GameObject rightImage = null;

	void AddInteractionText(Interactable interactor, Interaction interaction){
		if (interaction.HasText) {
			GameObject newText;
			textPanel.SetActive (true);
			if (interaction.HasImage) {
				ShowLeftImage (interaction.iImage);
			}
			if (interaction.HasImage2) {
				ShowRightImage (interaction.iImage2);
			}
			switch (interaction.iTextType) {
			case TextType.Monologue:
				newText = Instantiate (monologueDisplay) as GameObject;
				newText.transform.SetParent (textPanel.transform);
				newText.GetComponentInChildren<Text> ().text = interaction.iText;
				break;
			case TextType.Option:
				newText = Instantiate (optionDisplay) as GameObject;
				newText.transform.SetParent (textPanel.transform);
				newText.GetComponentInChildren<Text> ().text = interaction.iText;
				newText.GetComponent<InteractionButton> ().interactor = interactor;
				newText.GetComponent<InteractionButton> ().interaction = interaction;
				break;
			default:
				#if (DEBUG)
				Debug.Log (interaction.iName + " isn't an option or a monologue.");
				#endif
				break;
			}
		} else {
			#if (DEBUG)
			Debug.Log(interaction.iName + " in " + interactor.gameObject.name + " doesn't have text. Why are you trying to add it?");
			#endif
		}
	}

	void ShowLeftImage(Sprite image){
		leftImage.SetActive (true);
		leftImage.transform.FindChild ("InteractionImage").GetComponent<Image> ().sprite = image;
	}

	void ShowRightImage(Sprite image){
		rightImage.SetActive (true);
		rightImage.transform.FindChild ("InteractionImage").GetComponent<Image> ().sprite = image;
	}
		
	void ClearTextPanel(){
		List<Transform> textObjects = textPanel.GetComponentsInChildren<Transform> ().ToList ();
		foreach (Transform t in textObjects) {
			if (textPanel.transform != t) {
				Destroy (t.gameObject);
			}
		}
	}

	public void ClearInteractions(){
		ClearTextPanel ();
		GameManager.UIManager.DisableTapToContinue ();
		textPanel.SetActive (false);
		rightImage.SetActive (false);
		leftImage.SetActive (false);
	}

	public static void HandleClick(Interactable clicked){
		List<Interaction> clickInteractions = clicked.Interactions.FindAll (i => i.iType == InteractionType.Click);
		if(clicked.Debugging) {Debug.Log ("Clicked on " + clicked.gameObject.name + ". Valid interactions: " + string.Join (" ", clickInteractions.Select(i=>i.iName).ToArray ()));}
		HandleInteractionList (clicked, 
			clickInteractions);
	}

	public static void HandleUseItem(Interactable target){
		GameObject selected = GameManager.InventoryManager.Selected;
		if (selected != null) {
			List<Interaction> prevalidatedUseInteractions = target.Interactions.FindAll (i => i.iType == InteractionType.UseItem && i.IsValid);
			if (target.Debugging) {Debug.Log ("Used " + GameManager.InventoryManager.Selected.name +" on " + target.gameObject.name + ". Valid interactions: " + string.Join (" ", prevalidatedUseInteractions.Select(i=>i.iName).ToArray ()));}
			if (prevalidatedUseInteractions.Count > 0) {
				HandleInteractionList (target, prevalidatedUseInteractions);
			} else {
				Interaction defaultError = GameManager.InventoryManager.Selected.GetComponent<Interactable> ().Interactions.Find (i => i.iName == "DefaultCannotUse");
				if (defaultError != null) {
					HandleInteraction (selected.GetComponent<Interactable> (), defaultError);
				}
			}
		} else if(target.Debugging) {Debug.Log("There is no selected item to use on " + target.gameObject.name);}
	}

	public static void HandleOrange(Interactable orangeHit){
		List<Interaction> orangeInteractions = orangeHit.Interactions.FindAll (i => i.iType == InteractionType.Orange);
		if (orangeHit.Debugging) {Debug.Log ("Hit " + orangeHit.gameObject.name + " with an orange. Valid interactions: " + string.Join(" ", orangeInteractions.Select(i=>i.iName).ToArray()));}
		HandleInteractionList (orangeHit, orangeInteractions);
	}

	public static void HandleDrop(Interactable dropped){
		List<Interaction> droppedInteractions = dropped.Interactions.FindAll (i => i.iType == InteractionType.Orange);
		if (dropped.Debugging) {Debug.Log ("Dropped " + dropped.gameObject.name + ". Valid interactions: " + string.Join(" ", droppedInteractions.Select(i=>i.iName).ToArray()));}
		if (droppedInteractions.Count > 0) {
			HandleInteractionList (dropped, droppedInteractions);
		} else {
			dropped.DoSpecialActions (new List<string> { "ReturnSelected" });
		}
	}

	public static void HandleInteractionList(Interactable interactor, List<Interaction> interactionList){
		List<Interaction> validInteractions = interactionList.FindAll (i => i.IsValid);
		float interactionDistance = Vector3.Distance (interactor.transform.position, GameManager.PlayerCharacter.transform.position);
		#if (DEBUG)
		Debug.Log("Valid Interactions: " + string.Join(" ", validInteractions.Select(i=>i.iName).ToArray()));
		Debug.Log("Distance Threshold: " + interactionDistance.ToString());
		#endif
		List<Interaction> tooFar = validInteractions.FindAll (i => interactionDistance > i.iMaxDist);
		List<Interaction> closeEnough = validInteractions.Except (tooFar).ToList ();
		if (tooFar.Count == 0) {
			foreach (Interaction interaction in closeEnough) {
				if (interaction.HasText) {
					DisplayInteraction (interactor, interaction);
				} else {
					CompleteInteraction (interactor, interaction);
				}
			}
			List<Interaction> displayed = closeEnough.Where (i => i.HasText && i.iTextType != TextType.Floating).ToList();
			if(displayed.Count () == 1) {
				GameManager.UIManager.EnableTapToContinue (interactor, displayed.Single ());
			}
		} else {
			//from tooFar, find all interactions with an alternative, and from that get all interactions from the master list whose name matches that alternative, and add that to the close enough interactions.
			List<Interaction> alternatives = tooFar.Where (x => x.iTooFar != null).SelectMany (y => validInteractions.FindAll (z => z.iName == y.iTooFar)).Union(closeEnough).Distinct().ToList();
			HandleInteractionList (interactor, alternatives);
		}
	}

	public static void HandleInteraction(Interactable interactor, Interaction interaction){
		if (interaction.HasText) {
			DisplayInteraction (interactor, interaction);
		} else {
			CompleteInteraction (interactor, interaction);
		}
	}

	static void DisplayInteraction(Interactable interactor, Interaction interaction){
		if (interaction.HasText) {
			if (interaction.iTextType == TextType.Floating) {
				interactor.GetComponentInChildren<SpeechBubble> ().Say (interaction.iText);
			} else {
				GameManager.InteractionManager.AddInteractionText (interactor, interaction);
			}
		}
	}

	public static void CompleteInteraction(Interactable interactor, Interaction interaction){
		if (interactor.Debugging) {
			Debug.Log ("Completing " + interaction.iName + " for " + interactor.gameObject.name);
		}
		GameManager.TakeTags (interaction.iTakeTags);
		GameManager.GiveTags (interaction.iGiveTags);
		GameManager.InventoryManager.TakeItemList (interaction.iTakeItems);
		GameManager.InventoryManager.GiveItemList (interaction.iGiveItems);
		interactor.DoSpecialActions (interaction.iSpecialActions);
		if (interaction.HasNext) {
			Interactable nextInteractor = interaction.NextNotSelf ? GameObject.Find (interaction.iNextInteractor).GetComponent<Interactable> () : interactor;
			List<Interaction> nextInteractions = nextInteractor.Interactions.FindAll (i => i.iType == InteractionType.Derivative && i.iName == interaction.iNext);
			if (interactor.Debugging) {
				Debug.Log ("Getting next interactions for " + interaction.iName + ". Valid interactions: " + nextInteractions.Count);
			}
			HandleInteractionList (nextInteractor, nextInteractions);
		}
	}


}
