/*
 * Author: Donna Pan
 * Description: A class that holds information about merge.
 * Dependencies: Recipe.cs
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	public Recipe GetRecipe(string objective_item) {
		foreach (Recipe recipe in MergeList) {
			if(recipe.Objective.Equals(objective_item)) {
				return recipe;
			}
		}
		return null;
	}
}
