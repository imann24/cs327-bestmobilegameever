using UnityEngine;
using System.Collections;

public class DataTree {

	public DataNode Root;

	public int Depth {
		get {
			return Root.Depth();
		}
	}

	public int LeafNodeCount () {
		throw new System.NotImplementedException();
	}

	public void Add (string value) {
		Root.AddChild(value);
	}
}
