/*
 * Author: Isaiah Mann
 * Description: Static Utility class to interact with CSV documents
 */

using UnityEngine;
using System.Collections;

public static class CSVUtil {

	public static string [,] ReadCSV (string filePath) {
		string csvAsString = FileUtil.FileText(filePath);

		string[] byLine = csvAsString.Split('\n');

		int columns = GetMaxRowLength(byLine);
		int rows = byLine.Length;

		string[,] byCell = new string[columns, rows];

		for (int x = 0; x < columns; x++) {
			for (int y = 0; y < rows; y++) {
				byCell[x, y] = byLine[y].Split(',')[x];
			}
		}

		return byCell;
	}

	public static int GetMaxRowLength (string[] byLine) {
		int max = byLine[0].Split(',').Length;

		for (int i = 1; i < byLine.Length; i++) {
			max = Mathf.Max(max, byLine[i].Split(',').Length);
		}

		return max;
	}

}
