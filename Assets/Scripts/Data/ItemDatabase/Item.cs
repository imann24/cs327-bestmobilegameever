/*
 * Author: Donna Pan
 * Description: A class for storing an item
 */

using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item : MonoBehaviour {

	public string itemName;
	public string itemDescription;
	public Texture2D icon;
	public int itemCount;

	// stub for now; should read in some sort of data from controller(?) to fill in the fields...
	// do we even need this? no, right?
	public void initializeItem() {
		itemName = "stub";
		itemCount = 0;
		itemDescription = "none";
		icon = null;
	}

	// add 1 item
	public void addItem() {
		itemCount++;
	}

	// use 1 item
	public void removeItem() {
		itemCount--;
	}
}

