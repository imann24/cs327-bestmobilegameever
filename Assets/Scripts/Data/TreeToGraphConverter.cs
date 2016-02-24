using System;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]


public class TreeToGraphConverter
{
	public DataTree Tree;
	public DirectedGraph<string> DirectedGraph;
	public Queue<DataNode> Queue = new Queue<DataNode>() ;
	public TreeToGraphConverter (DataTree tree)
	{
		Tree = tree;
		DirectedGraph= new DirectedGraph<string>();
	}

	//convert a list of DataNodes to a list of GraphNode
	public List<DirectedGraphNode<string>> TreeListToGraphList(List<DataNode> list)
	{
		List<DirectedGraphNode<string>> ListToReturn = new List<DirectedGraphNode<string>> ();
		if(list!=null)
		{
			for (int i = 0; i < list.Count; i++) 
			{
				ListToReturn.Add(TreeToGraphNode(list[i]));

			}

			return ListToReturn;
		}
		return null;


	}
	//convert a DataNode to a DirectedGraphNode
	public DirectedGraphNode<string> TreeToGraphNode(DataNode node)
	{
		return new DirectedGraphNode<string> (node.Value);
	}

	/** converts a tree to graph using breadth first search**/
	public DirectedGraph<string> ConvertToGraph()
	{
		//check if the root of the tree is null
		if (Tree.Root != null) 
		{
			Queue.Enqueue (Tree.Root);
			int size = Queue.Count;
			while (size != 0) 
			{
				DataNode CurrNode = Queue.Dequeue ();
				List<DirectedGraphNode<string>> Neighbors = TreeListToGraphList (CurrNode.Children);
				DirectedGraphNode<string> GraphNode = TreeToGraphNode (CurrNode);
				DirectedGraph.AddNode (GraphNode);
				GraphNode.Neighbors = Neighbors;
				if (CurrNode.Children != null) {
					EnqueueList (CurrNode.Children);
				}
				size = Queue.Count;
			}

			return DirectedGraph;
		}
		return null;

	}



	/** enqueue an entire list**/
	public void EnqueueList(List<DataNode> list)
	{
		if (list != null) //check if the list is null
		{ 
			for (int i = 0; i < list.Count; i++) 
			{

				Queue.Enqueue (list [i]);

			}
		}

	}
	

}


