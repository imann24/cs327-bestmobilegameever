using System;
using UnityEngine;
using System.Collections;

public interface ConversationInterface
{

	/** get the first line of dialogue in the conversation**/
	DirectedGraphNode<string> GetFirstDialogue();

	/** get the current node we are at**/
	DirectedGraphNode<string> GetCurrentDialogue();

	/** get the current dialogue text**/
	string GetCurrentDialogueText();

	/** Sets the Dialogue to the new IConversationNode, if it is a valid response **/
	void AdvanceDialogue(DirectedGraphNode<string> Response);




}


