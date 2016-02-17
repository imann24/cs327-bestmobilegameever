using System;
using UnityEngine;
using System.Collections;

public interface ConversationInterface<T>
{

	/** get the first line of dialogue in the conversation**/
	DirectedGraphNode<T> GetFirstDialogue();

	/** get the current node we are at**/
	DirectedGraphNode<T> GetCurrentDialogue();

	/** get the current dialogue text**/
	string GetCurrentDialogueText();

	/** Sets the Dialogue to the new IConversationNode, if it is a valid response **/
	void AdvanceDialogue(DirectedGraphNode<T> Response);




}


