/*
 * Author: Isaiah Mann
 * Description: Utility scripts for handling XML files and classes
 */

using UnityEngine;
using System.Xml;
using System.Collections;

public static class XMLUtil {

	// For debugging purposes
	public static string ToString (XmlDocument document) {
		string xmlDocAsString = "";

		for (int i = 0; i < Length(document); i++) {
			xmlDocAsString += document.ChildNodes[i].Value + "\n";
		}

		return xmlDocAsString;
	}

	public static int Length (XmlDocument document) {
		return document.ChildNodes.Count;
	}
}
