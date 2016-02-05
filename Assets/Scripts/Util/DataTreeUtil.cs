/*
 * Author: Isaiah Mann
 * Description: Static functions to assist with the DataTree and DataNode classes
 */

using UnityEngine;
using System.Collections;

public static class DataTreeUtil {

	public static string [] GetChildNodeValues (DataNode node) {
		if (node.HasChildren) {
			string[] childrenValues = new string[node.ChildCount];

			for (int i = 0; i < node.ChildCount; i++) {
				childrenValues[i] = node.Children[i].Value;
			}

			return childrenValues;

		} else {
			return null;
		}
	}

}
