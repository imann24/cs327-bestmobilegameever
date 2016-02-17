/*
 * Author: Isaiah Mann
 * Description: A data tree class
 * Stores data as nodes with a Value represented as a string
 * Dependencies: DataNode.cs
 */

using UnityEngine;
using System.Collections;

public class DataTree {
	const string _defaultRootValue = "Root";

	// String indexer for accessing a property in grand child node:
	/* EXAMPLE: 
	 * this.Value == "DateTime"
	 * 		this.Children[i].Value == "Time"
	 * 			this.Children[i].Children[j].Value == "10:10"
	 * this["Time"] == "10:10"
	 */
	public string this[string keyInChildren] {
		get {
			return Root.GetFirstGrandChildValueByKey(keyInChildren);
		}
	}

	// Integer indexer for accessing a child, returns null if out of range
	public DataNode this[int index] {
		get {
			if (ListUtil.InRange(Root.Children, index)) {
				return Root.Children[index];
			} else {
				return null;
			}
		}
	}

	public DataNode Root;

	public DataTree () {
		Root = new DataNode(_defaultRootValue);
	}

	// How many nodes deep the tree is
	public int Depth {
		get {
			return Root.Depth();
		}
	}

	// How many children in the tree have no children of their own
	public int LeafCount () {

		return Root.LeafCount();

	}

	// Currently adds child nodes to the root, will think about revising to be able to add nodes to any child node
	public void Add (string value) {
		Root.AddChild(value);
	}

	public override string ToString () {
		return Root.ToString();
	}

	// Recursive: Returns first node it finds, or null if no node is found
	public DataNode SearchForNodeByValue (string value) {

		return Root.SearchForNodeByValue(value);

	}
}
