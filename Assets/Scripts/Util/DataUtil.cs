/*
 * Author: Isaiah Mann
 * Description: General data util and loading methods
 */
using UnityEngine;
using System.Collections;

public static class DataUtil {

	public static DataTree ParseXML (string filePathInResources) {
		return XMLReader.ReadXMLAsDataTree (
			XMLReader.Read (
				filePathInResources
			)
		);
	}

}
