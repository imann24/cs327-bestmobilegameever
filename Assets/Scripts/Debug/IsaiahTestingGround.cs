using UnityEngine;
using System.Xml;
using System.Collections;

public class IsaiahTestingGround : MonoBehaviour {

	// Use this for initialization
	void Start () {
		/*
		DataTree tree = new DataTree();

		tree.Root = new DataNode("Hi");

		tree.Add("Hello");
		tree.Add("Peace");

		tree.Root.Children[0].AddChild("Whatup");
		tree.Root.Children[0].Children[0].AddChild("Whatup");
		tree.Root.Children[0].Children[0].Children[0].AddChild("Whatup");

		Debug.Log(tree.Depth);
		Debug.Log(tree.Root);
		*/

		XmlDocument doc =  XMLReader.Read("Text/TestFile");

		Debug.Log("&&&&&");
		Debug.Log(
			XMLReader.ReadXMLAsDataTree(doc)
		);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
