/*
 * Author: Isaiah Mann
 * Description: A data tree class
 * Stores data as nodes with a Value represented as a string
 * Dependencies: DataNode.cs
 */

using UnityEngine;
using System.Collections;

public class DataTree {

	public DataNode Root;

	// How many nodes deep the tree is
	public int Depth {
		get {
			return Root.Depth();
		}
	}

	public int LeafNodeCount () {
		throw new System.NotImplementedException();
	}

	// Currently adds child nodes to the root, will think about revising to be able to add nodes to any child node
	public void Add (string value) {
		Root.AddChild(value);
	}
}
