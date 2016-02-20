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

	// Overloaded method to create a conversation directly from XML
	public Conversation (string pathFileInResources) {

		DataTree tree = DataUtil.ParseXML(pathFileInResources);

		DirectedGraph<string> graph = new TreeToGraphConverter(tree).ConvertToGraph();

		this.Graph = graph;

		Cursor = GetFirstDialogue();
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

	// Overloaded method to advance cursor automatically
	// And gets text in one method
	public string AdvanceDialogue() {
		if (Cursor.NeighborCount > 0) {
			Cursor = Cursor.Neighbors[0];
		}

		return GetCurrentDialogueText();
	}

	public bool HasNext () {
		return Cursor.NeighborCount > 0;
	}

}


