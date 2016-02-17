/*
 * Author: Donna Pan
 * Description: A class that holds information about merge.
 * Dependencies: Recipe.cs
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ItemMergeList : MonoBehaviour {

	const string LISTFILE = "Text/SampleMergeList";

	public List<Recipe> MergeList = new List<Recipe> ();

	public void InitMergeList() {
		DataTree recipeTree = DataUtil.ParseXML(LISTFILE);
		int i = 0;
		while (recipeTree [i] != null){ // reading recipes
			MergeList.Add(new Recipe(recipeTree[i]));
			i++;
		}
	}
		
	// stub until figure out return type
	public void GetRecipe(string objective_item) {
	}
}
