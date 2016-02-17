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
	public Dictionary<string,int> Ingredients = new Dictionary<string,int>();

	public Recipe(DataNode recipe) {
		Objective = recipe [0].Value;
		int i = 1;
		while (recipe [i] != null) {
			Ingredients.Add(recipe [i] [0].Value,int.Parse(recipe [i] [1].Value));
		}
	}
}
