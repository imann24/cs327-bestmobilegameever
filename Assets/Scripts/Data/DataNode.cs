/*
 * Author: Isaiah Mann
 * Description: A node within the tree data structure
 * Can have an arbitrary amount of children
 * Stores value as a string
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataNode {

	// String indexer for accessing a property in grand child node:
	/* EXAMPLE: 
	 * this.Value == "DateTime"
	 * 		this.Children[i].Value == "Time"
	 * 			this.Children[i].Children[j].Value == "10:10"
	 * this["Time"] == "10:10"
	 */
	public string this[string keyInChildren] {
		get {
			return GetFirstGrandChildValueByKey(keyInChildren);
		}
	}

	// Integer indexer for accessing a child, returns null if out of range
	public DataNode this[int index] {
		get {
			if (ListUtil.InRange(Children, index)) {
				return Children[index];
			} else {
				return null;
			}
		}
	}

	// Empty Constructor
	// CAUTION: should only use if you're going to set the values later
	public DataNode () {

	}

	// Constructor for the root node
	public DataNode (string value) {
		this.Value = value;
	}

	// Constructor for a child node
	public DataNode (DataNode parent, string value) {
		this.Parent = parent;
		this.Value = value;
	}

	public DataNode Parent;

	//WARNING: Wait to initialize this until it's needed to prevent a stack overflow
	public List<DataNode> Children;

	public string Value;

	public bool HasChildren {
		get {
			// Uses custom list function
			return ! ListUtil.IsNullOrEmpty(Children);
		}
	}

	public bool HasParent {
		get {
			return Parent != null;
		}
	}

	public int ChildCount {
		get {
			if (HasChildren) {
				return Children.Count;
			} else {
				return 0;
			}
		}
	}

	// Overloaded method that takes a string instead of a DataNode
	public void AddChild (string value) {
		DataNode newNode = new DataNode (
			this,
			value
		);

		AddChild (
			newNode
		);
	}

	public void AddChild (DataNode node) {
		if (Children == null) {
			initChildList();
		}

		Children.Add (
			node
		);

		node.Parent = this;
	}

	// Tree Depth from the current node
	public int Depth (int currentDepth) {

		if (HasChildren) {
			
			return maxDepthFromChildren(currentDepth);

		} else {
			return currentDepth + 1;
		}
	}

	// Tree Depth from the current node
	public int Depth () {
		if (HasChildren) {
			return maxDepthFromChildren(0);
		} else {
			return 1;
		}
	}

	// How deep in the tree the node is
	public int NodeDepth (int depth = 1) {
		if (HasParent) {
			return Parent.NodeDepth(depth+1);
		} else {
			return depth;
		}
	}

	// For debugging purposes: represents nesting through use of tab cahracters
	public override string ToString () {
		return string.Format ("[DataNode: Value={0},\n" +
			StringUtil.RepeatString("\t", NodeDepth()) +
			"Children={1}]", Value, ListUtil.ToString(Children));
	}
		
	// Recursive: Returns the object it was called on if the parent is null
	public DataNode Root () {
		if (Parent == null) {
			return this;
		} else {
			return Parent.Root();
		}
	}

	public string GetFirstGrandChildValueByKey (string key) {
		try {
			return GetGrandChildrenValuesByKey(key)[0];
		} catch {
			return null;
		}
	}
		
	public DataNode[] GetGrandChildrenByKey (string key) {
		for (int i = 0; i < ChildCount; i++) {
			if (Children[i].Value == key && Children[i].HasChildren) {
				return Children[i].Children.ToArray();
			}
		}

		return null;
	}

	public string[] GetGrandChildrenValuesByKey (string key) {
		for (int i = 0; i < ChildCount; i++) {
			if (Children[i].Value == key && Children[i].HasChildren) {
				return (childValues(Children[i]));
			}
		}

		return null;
	}

	// Recursive: Returns first node it finds, or null if no node is found
	public DataNode SearchForNodeByValue (string value) {
		if (this.Value == value) {
			return this;
		} else if (HasChildren) {

			DataNode[] resultNodes = new DataNode[ChildCount];

			for (int i = 0; i < resultNodes.Length; i++) {
				resultNodes[i] = Children[i].SearchForNodeByValue(value);
			}

			return tryReturnFirstNonNullNode (
				resultNodes
			);

		} else {
			return null;
		}
	}

	DataNode tryReturnFirstNonNullNode (params DataNode[] nodes) {
		for (int i = 0; i < nodes.Length; i++) {
			if (nodes[i] != null) {
				return nodes[i];
			}
		}

		return null;
	}

	// Finds the greatest depth: to be coupled with the Depth function recursively
	int maxDepthFromChildren (int currentDepth) {

		int [] childrenMaxDepths = new int[Children.Count];

		for (int i = 0; i < Children.Count; i++) {
			childrenMaxDepths[i] = Children[i].Depth(currentDepth);
		}	

		/* 
		* Passes all the child depths into the Mathf.Max function:
		* can take an arbitrary amount of numbers and return the max
		*/
		return Mathf.Max (
			childrenMaxDepths
		) + 1;
	}

	// A helper method because it would cause stack overflow to initialize Children in the variable header
	void initChildList () {
		Children = new List<DataNode>();
	}

	// How many children in the tree (or this branch) have no children of their own
	public int LeafCount () {
		if (HasChildren) {
			int sum = 0;

			for (int i = 0; i < ChildCount; i++) {
				sum += Children[i].LeafCount();
			}

			return sum;
				
		} else {
			return 1;
		}
	}

	string [] childValues (DataNode parentNode) {
		string [] values = new string[parentNode.ChildCount];

		for (int i = 0; i < values.Length; i++) {
			values[i] = parentNode[i].Value;
		}

		return values;
	}
}