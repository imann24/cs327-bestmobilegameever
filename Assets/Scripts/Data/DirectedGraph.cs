using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class DirectedGraph<T>
{
	public List<DirectedGraphNode<T>> NodeList;

	public DirectedGraph(List<DirectedGraphNode<T>> NodeList)
	{
		this.NodeList = NodeList;

	}
	public DirectedGraph()
	{
		NodeList = new List<DirectedGraphNode<T>> ();
	}


	public void AddNode(DirectedGraphNode<T> node)
	{
		if (!NodeList.Contains (node)) 
		{ //we only add it if the node list does not already have the node
			NodeList.Add (node);

		}
	}

	public void AddNode(T value)
	{
		AddNode (new DirectedGraphNode<T> (value));
	}


	/** add an edge, does not add edge if there exists an edge between the two nodes**/
	public void AddEdge(DirectedGraphNode<T> from, DirectedGraphNode<T> to)
	{
		if (NodeList.Contains (from)) 
		{
			if (!from.HasNeighbor (to)) {  //we only add if from does not have neighbor to
				from.AddNeighbor (to);
			}
		} 
		else  //when the NodeList doesn't contain from
		{
			AddNode (from); //add it to the graph
			from.AddNeighbor(to);
		}

	}

	/** checks to see if the graph has an edge from fromNode to to node**/
	public bool HasEdge(DirectedGraphNode<T> from, DirectedGraphNode<T> to )
	{
		if (!NodeList.Contains (from)) {
			return false;
		} 

		return from.HasNeighbor (to);

	}

	//checks if a certain node exists in the graph
	public bool Contains(DirectedGraphNode<T> Node) 
	{
		return NodeList.Contains(Node);
	}

	//check if a certain value exists in the graph
	public bool Contains(T value) 
	{
		for(int i=0;i<	NodeList.Count;i++)
		{
			if(NodeList[i].Value.Equals(value))
			{
				return true;
			}
		}
		return false;
	}


	//finds the index of a value, returns -1 if not found

	public int IndexOf(T value)
	{
		for(int i=0; i<NodeList.Count ; i++)
		{
			if(NodeList[i].Value.Equals(value))
			{
				return i;
			}

		}
		return -1;

	}

	public List<DirectedGraphNode<T>> GetNodeList()
	{
		return NodeList;
	}


	public int Size()
	{
		return NodeList.Count;
	}







}



