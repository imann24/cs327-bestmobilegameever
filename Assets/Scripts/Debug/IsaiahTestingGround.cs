using UnityEngine;
using System.Xml;
using System.Xml.Linq;
using System.Collections;

public class IsaiahTestingGround : MonoBehaviour {

	// Use this for initialization
	void Start () {

		DataTree tree = new DataTree();

		tree.Root = new DataNode("Hi");

		tree.Add("Hello");
		tree.Add("Peace");

		tree.Root.Children[0].AddChild("Whatup");
		tree.Root.Children[0].Children[0].AddChild("Whatup");
		tree.Root.Children[0].Children[0].Children[0].AddChild("Whatup");
		Debug.Log(tree.Depth);
		Debug.Log(tree.Root);
		XmlDocument doc =  XMLReader.Read("Text/TestFile");

		Debug.Log(doc.ChildNodes[0].Value);
		Debug.Log(
			XMLUtil.ToString (
				doc
			)
		);

		XmlDocument doc2 = new XmlDocument();
		doc2.LoadXml(@"<item><name>wrench</name></item>");
		Debug.Log(doc2.OuterXml);
		Debug.Log(doc2.Value);

		Debug.Log (
			XMLUtil.ToString (
				XMLReader.ReadFromString (
					"<item><name>wrench</name></item>"
				)
			)
		);

		XDocument xDoc = XMLReader.ReadAsXDocument("Text/TestFile");

		Debug.Log(xDoc.Element("title"));
		Debug.Log(xDoc.Document.Elements());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
