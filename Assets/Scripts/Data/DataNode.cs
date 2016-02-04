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
}
