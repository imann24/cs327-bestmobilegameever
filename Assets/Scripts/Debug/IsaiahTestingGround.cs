/*
 * Author: Isaiah Mann
 * Description: Debugging/demonstration script (like a debugging Main function)
 */
using UnityEngine;
using System.Xml;
using System.Collections;

public class IsaiahTestingGround : MonoBehaviour {

	// Use this for initialization
	void Start () {

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

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
