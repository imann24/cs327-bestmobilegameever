/*
 * Author: Isaiah Mann
 * Decription: Abstract representation of a single line of dialogue
 */

using UnityEngine;
using System.Collections;

public interface IConversationNode {

	// Responses to this line of dialogue (there could only be one response in some cases)
	IList GetResponses ();

	// The actual text of this dialogue
	string GetText ();

	// Whether or not there are any responses this node (false if it is the last line in a conversatino
	bool HasResponses ();
}
