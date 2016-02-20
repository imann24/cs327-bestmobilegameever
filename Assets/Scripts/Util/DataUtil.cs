/*
 * Author: Isaiah Mann
 * Description: General data util and loading methods
 */
using UnityEngine;
using System.Collections;

public static class DataUtil {

	// Use this to load in XML Files
	// the file path starts at the "Resources" folder
	public static DataTree ParseXML (string filePathInResources) {
		return XMLReader.ReadXMLAsDataTree (
			XMLReader.Read (
				filePathInResources
			)
		);
	}

}