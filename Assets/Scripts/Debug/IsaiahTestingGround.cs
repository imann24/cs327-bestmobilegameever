using UnityEngine;
using System.Xml;
using System.Collections;

public class IsaiahTestingGround : MonoBehaviour {

	// Use this for initialization
	void Start () {
		XmlDocument doc =  XMLReader.Read("Text/TestFile");

		Debug.Log(doc.ChildNodes[0].Value);
		Debug.Log(
			XMLUtil.ToString (
				doc
			)
		);

		XmlDocument doc2 = new XmlDocument();
		doc2.LoadXml("<item><name>wrench</name></item>");
		Debug.Log(doc2.OuterXml);
		Debug.Log(doc2.Value);

		Debug.Log (
			XMLUtil.ToString (
				XMLReader.ReadFromString (
					"<item><name>wrench</name></item>"
				)
			)
		);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
