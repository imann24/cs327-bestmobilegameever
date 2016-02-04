using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataNode {

	public DataNode (string value) {
		this.Value = value;
	}

	public DataNode (DataNode parent, string value) {
		this.Parent = parent;
		this.Value = value;
	}

	public DataNode Parent;

	public List<DataNode> Children;

	public string Value;

	public bool HasChildren {
		get {
			return ! ListUtil.IsNullOrEmpty(Children);
		}
	}

	public bool HasParent {
		get {
			return Parent != null;
		}
	}

	public void AddChild (string value) {
		AddChild (
			new DataNode (
				this,
				value
			)
		);
	}

	public void AddChild (DataNode node) {
		if (Children == null) {
			initChildList();
		}

		Children.Add (
			node
		);
	}

	public int Depth (int currentDepth) {
		Debug.Log(Value);

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

		return Mathf.Max (
			childrenMaxDepths
		) + 1;
	}

	void initChildList () {
		Children = new List<DataNode>();
	}
}
