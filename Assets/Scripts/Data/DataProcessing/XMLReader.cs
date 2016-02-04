/*
 * Author: Isaiah Mann
 * Used to read in XML documents from text
 */
using UnityEngine;
using System.Xml;
using System.Collections;

public static class XMLReader {

	public static XmlDocument Read (string filePath) {
		return Read(Resources.Load<TextAsset>(filePath));
	}

	public static XmlDocument Read (TextAsset xmlDoc) {
		return ReadFromString (xmlDoc.text);
	}

	public static XmlDocument ReadFromString (string xmlAsString) {

		XmlDocument document = new XmlDocument();

		document.LoadXml(xmlAsString);

		return document;
	}
}
