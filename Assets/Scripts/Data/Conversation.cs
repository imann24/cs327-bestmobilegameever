using System;
using UnityEngine;
using System.Collections;
[System.Serializable];
public class Conversation
{
	public DataTree Tree;
	public DataNode Cursor;
		
	public Conversation (DataTree t)
	{
		Tree = t;
		Cursor = Tree.Root;
	}

	//Go back to the previous dialogue
	public void ReturnToParent()
	{
		Cursor = Cursor.Parent;
	}

	//Get the dialogue preceding the current dialogue
	public DataNode GetParentDialogue()
	{
		return Cursor.Parent;
	}

	//Get the string preceding current string
	public string GetParentDialogue()
	{
		return Cursor.Parent.Value;

	}

	// This first line of dialogue in this conversation
	public DataNode GetFirstDialogue()
	{
		return Tree.Root;
	}

	// The current line of dialogue, this is changed when you call AdvanceDialogue
	public DataNode GetCurrentDialogue()
	{
		if (Cursor != null) {
			return Cursor;
		}
		return null;
	}


	public string GetCurrentDialogueText()
	{
		return Cursor.Value;
	}

	//
	public void AdvanceDialogue(DataNode Response)
	{
		if (Response != null) {
			Cursor = Response;
		}

	}
}



