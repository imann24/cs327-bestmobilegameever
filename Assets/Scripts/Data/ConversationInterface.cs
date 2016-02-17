using System;
using UnityEngine;
using System.Collections;

<<<<<<< HEAD
/**
public interface ConversationInterface
{

	/** get the first line of dialogue in the conversation
	DirectedGraphNode<T> GetFirstDialogue();

	/** get the current node we are at
	DirectedGraphNode<T> GetCurrentDialogue();
=======
public interface ConversationInterface
{

	/** get the first line of dialogue in the conversation**/
	DirectedGraphNode<string> GetFirstDialogue();

	/** get the current node we are at**/
	DirectedGraphNode<string> GetCurrentDialogue();
>>>>>>> origin/master

	/** get the current dialogue text
	string GetCurrentDialogueText();

<<<<<<< HEAD
	/** Sets the Dialogue to the new IConversationNode, if it is a valid response
	void AdvanceDialogue(DirectedGraphNode<T> Response);
=======
	/** Sets the Dialogue to the new IConversationNode, if it is a valid response **/
	void AdvanceDialogue(DirectedGraphNode<string> Response);
>>>>>>> origin/master




}**/


