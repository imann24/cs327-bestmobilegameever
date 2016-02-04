/*
 * Author: Isaiah Mann
 * Used to read in XML documents from text
 */
using UnityEngine;
using System.Xml;
using System.Xml.Linq;
using System.Collections;

public static class XMLReader {
	const char _openingChevron = '<';
	const char _closingChevron = '>';

	public static XmlDocument Read (string filePath) {
		return Read(Resources.Load<TextAsset>(filePath));
	}

	public static XmlDocument Read (TextAsset xmlDoc) {
		return ReadFromString (xmlDoc.text);
	}

	public static XDocument ReadAsXDocument (string filePath) {
		return ReadAsXDocument(Resources.Load<TextAsset>(filePath));
	}

	public static XDocument ReadAsXDocument (TextAsset xmlDoc) {
		return ReadFromStringAsXDocument(xmlDoc.text);
	}

	public static XmlDocument ReadFromString (string xmlAsString) {

		XmlDocument document = new XmlDocument();

		document.LoadXml(xmlAsString);


		XmlNodeList nodes = document.ChildNodes;


		foreach (XmlNode node in nodes) {
			Debug.Log("****");
			Debug.Log(node.ChildNodes[0].ChildNodes[0].Value);
		}

		return document;
	}

	public static DataTree ReadXMLAsDataTree (XmlDocument document) {
		DataTree tree = new DataTree();
		DataNode root = tree.Root;

		// Recursive Call
		ReadNode (document.DocumentElement, ref root);

		return tree;
	}

	// Recursive Function
	public static void ReadNode (XmlNode xmlNode, ref DataNode dataWriteNode) {
		dataWriteNode.Value = 
			xmlNode.Value == null ?
			getLeadingTag(xmlNode) :
			xmlNode.Value;
		
		Debug.Log("^^^");
		Debug.Log(
			xmlNode.Value
		);
		if (xmlNode.HasChildNodes) {

			DataNode newDataNode = new DataNode("");

			dataWriteNode.AddChild (newDataNode);

			foreach (XmlNode childXMLNode in xmlNode.ChildNodes) {
				ReadNode(
					childXMLNode,
					ref newDataNode
				);
			}
		}
	}

	static bool hasValue (XmlNode xmlNode) {
		return xmlNode.Value != null;
	}

	static string getLeadingTag (XmlNode xmlNode) {
		if (beginsWithTag(xmlNode)) {

			string xmlNodeAsString = xmlNode.OuterXml;

			int stringPointer = 1;

			while (xmlNodeAsString[stringPointer] != _closingChevron) {
				stringPointer++;
			}

			return xmlNodeAsString.Substring(1, stringPointer-1);

		} else {
			
			Debug.LogWarning("XML code invalid. No tag found");

			return null;

		}
			
	}

	static bool beginsWithTag (XmlNode xmlNode) {
		string xmlAsString = xmlNode.OuterXml;

		if (xmlAsString[0] != _openingChevron) {
			return false;
		}

		for (int i = 1; i < xmlAsString.Length; i++) {
			if (xmlAsString[i] == _closingChevron) {
				return true;
			}
		}

		return false;
	}


	public static XDocument ReadFromStringAsXDocument (string xmlAsString) {
		return XDocument.Parse(xmlAsString);
	}
}
