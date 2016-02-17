/*
 * Author: Donna Pan
 * Description: A class that holds recipes.
 * Dependencies: --
 */

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Recipe : MonoBehaviour {
	
	public string Objective;
	public List<Dictionary<string,int>> Ingredients = new List<Dictionary<string,int>>();

	public Recipe(DataNode recipe) {
		Objective = recipe [0].Value;
		int i = 1;
		while (recipe [i] != null) {
			Dictionary<string,int> ingredient = new Dictionary<string,int> (recipe [i] [0], recipe [i] [1]);
			Ingredients.Add (ingredient);
		}
	}
}
