/*
 * Author: Donna Pan
 * Description: A class for storing an item
 */

using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item : MonoBehaviour {

	public string ItemName;
	public string ItemDescription;
	public int ItemCount;
	//enum for item type

	[System.NonSerialized] // Texture2D's are non-serializable
	Texture2D Icon;

	// stub for now; should read in some sort of data from controller(?) to fill in the fields...
	// do we even need this? no, right?
	public void InitializeItem() {
		ItemName = "stub";
		ItemCount = 0;
		ItemDescription = "none";
		Icon = null;
	}

	// add 1 item
	public void AddItem() {
		ItemCount++;
	}

	// use 1 item
	public void RemoveItem() {
		ItemCount--;
	}
}

