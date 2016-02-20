/*
 * Author: Isaiah Mann
 * Description: Debugging/demonstration script (like a debugging Main function)
 */
using UnityEngine;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

public class IsaiahTestingGround : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	// Used to test the visual conversation class
	void testConversationDisplay () {

		ConversationDisplayController conversationDisplay = ConversationDisplayController.Instance;

		List<DirectedGraphNode<string>> nodeList = new List<DirectedGraphNode<string>>();

		// Generates the conversation nodees
		DirectedGraphNode<string> pointer;
		DirectedGraphNode<string> previous = null;
		string [] testPhrases = new string[]{
			"Hi, Bob",
			"how are you?",
			"Good bye"
		};

		for (int i = 0; i < 3; i++) {
			pointer = new DirectedGraphNode<string>(testPhrases[i]);
			nodeList.Add(pointer);
			if (previous != null) {
				previous.AddNeighbor(pointer);
			}
			previous = pointer;
		}
			
		DirectedGraph<string> conversationGraph = new DirectedGraph<string>(nodeList);

		Conversation conversation = new Conversation(conversationGraph);

		conversationDisplay.Show();

		conversationDisplay.SetCharacter("Peter", Resources.Load<Sprite>("Visual/pirate"), ScreenPosition.Right);

		conversationDisplay.SetConversation(conversation);

	}

	// Testing Conversation class
	void testConversation () {

		Conversation conversation = new Conversation("Text/SampleDialogue");

		Debug.Log(conversation.GetCurrentDialogueText());
	
	}

	// Test randomized queue
	void randomizedQeueDemo () {
		string[] testList = new string[]{"one", "two", "three"};

		RandomizedQueue<string> queue = new RandomizedQueue<string>(testList);

		// Cycle ensures that the randomized queue does not return the same value two times in a row
		// But it also re-adds the value
		for (int i = 0; i < 20; i++) {
			Debug.Log(queue.Cycle());
		}
	}

	// For testing AudioController class
	void playSoundDemo () {
		EventController.Event("testStart");
		EventController.Event(PSEventType.StartMusic);
	}
		
	// Demonstration of the functionality of the DataTree class
	void dataTreeDemonstration () {

		// If you're not familiar with the data structure of a tree: http://www.tutorialspoint.com/data_structures_algorithms/tree_data_structure.htm

		// This is the funciton you can use to load any XML file stored inside the Resources folder
		DataTree tree = DataUtil.ParseXML("Text/TestFile");

		// You can print all the tree data to console because the ToString method I implemented (for debugging purposes)
		Debug.Log(tree);

		// This debugging also works on individual nodes
		Debug.Log(tree.Root);

		// You can index through the children of the root node
		Debug.Log(tree[1].Value);

		// You can also index through individual the children of individual nodes (ask me if you don't understand the nested indexing going on here)
		Debug.Log(tree[1][2].Value);

		// You can index through the tree's immediate children by giving a value. This looks for the first grandchild of a child node with the Value of "title"
		Debug.Log(tree["title"]);

		// You can combine the integer indexing and the string indexing
		Debug.Log(tree[1]["data-point"]);

		// How many children w/out children of their own are in the tree
		Debug.Log(tree.LeafCount());

	}

	// Update is called once per frame
	void Update () {
	
	}
}
