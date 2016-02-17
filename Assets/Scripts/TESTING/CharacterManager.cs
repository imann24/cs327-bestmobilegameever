using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

enum Interaction_Type{
	ITEM_USE,
	DIALOGUE,
	ORANGE_HIT}

public struct CharacterInteraction{
	public string interactionName;
	public string interactionType;
	public List<string> reqTagsAND;
	public List<string> reqTagsOR;
	public string dialogueLine;
	public string dialogueSpeaker;
	public string dialogueNext;
	public List<string> givenTags;
	public List<string> takenTags;
	public List<string> givenItems;
	public List<string> takenItems;
	public string triggerEvent;
}

public class CharacterManager{

	private XmlDocument dialogueXml = new XmlDocument ();
	private Dictionary<string,List<CharacterInteraction>> characterInteractions = new Dictionary<string, List<CharacterInteraction>>();

	private static CharacterManager instance;
	public static CharacterManager Instance {
		get {
			if (instance == null) {
				instance = new CharacterManager ();
			}
			return instance;
		}
	}

	private CharacterManager(){
		dialogueXml.Load ("Assets/Resources/Text/CharacterInteractions.xml");
		XmlNodeList xmlChars = dialogueXml.SelectSingleNode ("CharacterInteractions").ChildNodes; //all the nodes just inside CharacterInteractions, so Shipmaster, FirstMate, etc...
		foreach (XmlNode xmlChar in xmlChars) {
			Debug.Log (xmlChar.LocalName);
			List<CharacterInteraction> interactions = new List<CharacterInteraction> ();
			XmlNodeList xmlInteractions = xmlChar.SelectNodes ("Interaction");
			Debug.Log (xmlInteractions.Count);
			//foreach (XmlNode interaction in xmlInteractions){
			//characterInteractions.Add(
		}
	}
		
		
		
}
