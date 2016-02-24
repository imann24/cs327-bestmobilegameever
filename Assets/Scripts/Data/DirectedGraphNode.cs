/*
 * Author: Isaiah Mann
 * Description: A class to implement a node for a DirectedGraph
 * Notes: Made it generic in case we want to store something that is not a string
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DirectedGraphNode<T> {

	T _value;

	public T Value {
		get {
			return _value;
		}

		set {
			_value = value;
		}
	}

	// The neighbors of the node
	public List<DirectedGraphNode<T>> Neighbors;


	// Get a neighbor node by index
	public DirectedGraphNode<T> this[int index] {
		get {
			if (ListUtil.InRange(Neighbors, index)) {

				return Neighbors[index];

			} else {

				return null;

			}
		}
	}
		
	public int NeighborCount {
		get {
			if (ListUtil.IsNullOrEmpty(Neighbors)) {
				return 0;
			} else {
				return Neighbors.Count;
			}
		}
	}

	// Get a neighbor node by value
	public DirectedGraphNode<T> this[T valueOfNeighbor] {
		get {
			return FindNeighborWithValue(valueOfNeighbor);
		}
	}

	public DirectedGraphNode (T value) {

		this._value = value;
	}


	// Can pass in an arbitrary amount of nodes into the constructor that become neighbors of this node
	public DirectedGraphNode (T value, params DirectedGraphNode<T>[] neighbors) {

		this._value = value;

		AddNeighbors(neighbors);
	}

	public DirectedGraphNode () {}

	public void AddNeighbor (DirectedGraphNode<T> neighbor) {

		checkNeighborsList();

		Neighbors.Add(neighbor);
	}

	public void AddNeighbors (DirectedGraphNode<T>[] neighbors) {
		for (int i = 0; i < neighbors.Length; i++) {
			AddNeighbor(neighbors[i]);
		}
	}
	public bool HasNeighbor (DirectedGraphNode<T> neighbor) {
		return HasNeighbor(neighbor._value);
	}

	public bool HasNeighbor (T valueOfNeighbor) {
		for (int i = 0; i < Neighbors.Count; i++) {
			if (Neighbors[i]._value.Equals(valueOfNeighbor)) {
				return true;
			}
		}

		return false;
	}

	// Only returns first neighbor with matching value
	public DirectedGraphNode<T> FindNeighborWithValue (T valueOfNeighbor) {

		for (int i = 0; i < Neighbors.Count; i++) {
			if (Neighbors[i].Value.Equals(Value)) {
				return Neighbors[i];
			}
		}

		// If not found
		return null;
	}

	void checkNeighborsList () {
		if (Neighbors == null) {
			Neighbors = new List<DirectedGraphNode<T>>();
		}
	}
}
