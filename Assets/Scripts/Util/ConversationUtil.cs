/*
 * Author: Isaiah Mann
 * Description: Used to assist with operations for creating the conversation class
 */

using UnityEngine;
using System.Collections;

public static class ConversationUtil {

	const string SPEAKER_TAG = "speaker";
	const string TEXT_TAG = "text";
	const string RESPONSES_TAG = "responses";
	const string INDIVIDUAL_RESPONSE_TAG = "response";

	static bool DEBUG = false;

	// Returns the head node of the conversation class
	public static ConversationNode DataTreeToConversationNodes (DataTree conversationAsTree) {
		ConversationNode head = null;
		ConversationNode previous = null;
		ConversationNode pointer;

		for (int i = 0; i < conversationAsTree.Root.ChildCount; i++) {
			pointer = DataNodeToConversationNode(conversationAsTree[i]);

			if (head == null) {
				head = pointer;
			}

			if (previous != null) {
				previous.Next = pointer;
			}

			if (DEBUG) {
				Debug.Log(pointer);
				Debug.Log(pointer.Responses[0]);
			}

			previous = pointer;
		}

		return head;
	}

	public static ConversationNode DataNodeToConversationNode (DataNode node) {
		return new ConversationNode (
			node[SPEAKER_TAG],
			node[TEXT_TAG],
			GetResponsesAsArray(node)
		);
	}

	public static string [] GetResponsesAsArray (DataNode node) {
		DataNode[] responseNodes = node.GetGrandChildrenByKey(RESPONSES_TAG);

		string [] responses = new string[responseNodes.Length];
			
		for (int i = 0; i < responses.Length; i++) {
			responses[i] = responseNodes[i][0].Value;
		}

		return responses;


	}
}
