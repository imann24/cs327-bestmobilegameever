/*
 * Author: Isaiah Mann
 * Used to read in XML documents from text
 */
using UnityEngine;
using System.Xml;
using System.Xml.Linq;
using System.Collections;

public static class XMLReader {

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

	public static XDocument ReadFromStringAsXDocument (string xmlAsString) {
		return XDocument.Parse(xmlAsString);
	}
}
