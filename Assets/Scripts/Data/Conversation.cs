using System;
using UnityEngine;
using System.Collections;
[System.Serializable]
public class Conversation : ConversationInterface
{
	DirectedGraph<string> Graph;
	DirectedGraphNode<string> Cursor;

	public Conversation (DirectedGraph<string> Graph)
	{
		this.Graph = Graph;
		Cursor = GetFirstDialogue (); 
	}

	public DirectedGraphNode<string> GetFirstDialogue()
	{
		return Graph.NodeList [0];
	}

	public DirectedGraphNode<string> GetCurrentDialogue()
	{
		return Cursor;

	}

	public string GetCurrentDialogueText()
	{

		return Cursor.Value;
	}

	public void AdvanceDialogue(DirectedGraphNode<string> Response)
	{
		if (Response != null) {
			Cursor = Response;
		}
	}


}


