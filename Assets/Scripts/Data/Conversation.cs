/*
 * Authors: Isaiah Mann, Kaya Ni
 * Description: Used to store a dialogue interaction
 */

using System;
using UnityEngine;
using System.Collections;
[System.Serializable]
public class Conversation {
	ConversationNode Head;
	ConversationNode Cursor;

	public int Length {
		get {
			if (Head == null) {
				return 0;
			} else {
				return Head.Count();
			}
		}
	}

	public Conversation (ConversationNode head) {
		initFromHeadNode(head);
	}

	// Overloaded method to create a conversation directly from XML
	public Conversation (string pathFileInResources) {

		DataTree conversationAsTree = DataUtil.ParseXML(pathFileInResources);

		initFromHeadNode ( 
			ConversationUtil.DataTreeToConversationNodes (
				conversationAsTree
			)
		);
	}

	void initFromHeadNode (ConversationNode head) {
		this.Head = head;
		this.Cursor = head;
	}

	public string GetCurrentDialogue() {
		return Cursor.Text;
	}

	public string GetCurrentSpeaker() {
		return Cursor.SpeakerName;
	}

	public string[] GetCurrentResponses () {
		return Cursor.Responses;
	}
		

	public string AdvanceDialogue() {
		if (Cursor.HasNext) {
			Cursor = Cursor.Next;
		}

		return GetCurrentDialogue();
	}

	public bool HasNext () {
		return Cursor.HasNext;
	}

}


