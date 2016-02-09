/*
 * Author: Isaiah Mann
 * Description: Abstract representation of the conversation class
 */

using UnityEngine;
using System.Collections;

public interface IConversation {

	// This first line of dialogue in this conversation
	IConversationNode GetFirstDialogue ();

	// The current line of dialogue, this is changed when you call AdvanceDialogue
	IConversationNode GetCurrentDialogue ();

	// Can call the corresponding method inside IConversationNode
	string GetCurrentDialogueText ();

	// Sets the Dialogue to the new IConversationNode, if it is a valid response
	void AdvanceDialogue (IConversationNode Response);
}
