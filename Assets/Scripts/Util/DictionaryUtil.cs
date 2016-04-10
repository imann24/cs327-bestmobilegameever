using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class DictionaryUtil {

	public static string ToString<K, E> (Dictionary<K, E> source) {
		string dictionaryAsString = "{";

		foreach (KeyValuePair<K, E> entry in source) {
			dictionaryAsString += " " + entry.Key.ToString() + ": " + entry.Value + ",";
		}

		dictionaryAsString = dictionaryAsString.TrimEnd(',');
		return dictionaryAsString + "}";
	}
}
