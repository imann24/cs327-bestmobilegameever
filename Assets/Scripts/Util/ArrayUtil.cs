/*
 * Author(s): Isaiah Mann 
 * Description: Static class with array helper functions
 */

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class ArrayUtil {

	public static string [] RemoveEmptyElements (string [] original) {
		List<string> modified = new List<string>();

		for (int i = 0; i < original.Length; i++) {
			if (!string.IsNullOrEmpty(original[i]) && 
				original[i].Trim().Length != 0 &&
				original[i][0] != '\r' && 
				original[i][0] != '\n') {
				modified.Add(original[i]);
			}
		}

		return modified.ToArray();
	}

	public static T[] RemoveFirst<T> (T[] source) {
		T[] modified = new T[source.Length - 1];

		Array.Copy(
			source,
			1,
			modified,
			0,
			modified.Length);

		return modified;
	}

	public static T Pop<T> (ref T[] arrayToModify) {
		T firstElement = arrayToModify[0];
		arrayToModify = RemoveFirst(arrayToModify);
		return firstElement;
	}

	public static T[] Concat<T> (T[] source1, T[] source2) {
		T[] combined = new T[source1.Length + source2.Length];

		Array.Copy(source1, combined, source1.Length);
		Array.Copy(source2, 0, combined, source1.Length, source2.Length);

		return combined;
	}

	public static string ToString<T> (T[] source) {
		string arrayAsString = "";

		for (int i = 0; i < source.Length; i++) {
			arrayAsString += source[i] + ",\n";
		}

		return arrayAsString;
	}

	public static int IndexOf<T> (T[] source, T element) where T : IComparable {
		for (int i = 0; i < source.Length; i++) {
			if (source[i].CompareTo(element) == 0) {
				return i;
			}
		}

		throw new KeyNotFoundException();
	}

	public static T Remove<T> (ref T[]source, T element) where T : IComparable {
		int index = IndexOf(
			source,
			element);

		T[] modified = new T[source.Length-1];

		Array.ConstrainedCopy (
			source,
			0,
			modified,
			0,
			index-1);

		Array.ConstrainedCopy (
			source,
			index + 1,
			modified,
			index,
			source.Length - index - 1
		);

		return element;
	}


	public static void Add<T> (ref T[]source, T element) {
		T[] modified = new T[source.Length+1];
		modified[source.Length] = element;
	}

	public static string ToString<T> (List<T>[] source) {
		string result = string.Empty;

		for (int i = 0; i < source.Length; i++) {
			result +=  (i+1) + ". {\n" + ToString(source[i].ToArray()) + "}\n"; 
		}

		return result;
	}

	public static bool Contains<T> (T[] source, T element) where T : IComparable {
		for (int i = 0; i < source.Length; i++) {
			if (source[i].CompareTo(element) == 0) {
				return true;
			}
		}

		return false;
	}


}