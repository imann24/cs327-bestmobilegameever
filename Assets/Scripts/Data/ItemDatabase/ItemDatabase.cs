/*
 * Author: Donna Pan
 * Description: A class that holds all the items
 * Dependencies: Item.cs
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ItemDatabase : MonoBehaviour {

	public List<Item> items = new List<Item>();

	// stub for now; should read item information from the controller(?) in a certain format
	// and addItem to the list of items
	// But we should just do it via the Unity interface...
	public void initItemDatabase() {
		throw new System.NotImplementedException();
	}

	public Item getItem(string itemName) {
		return items.Find(x => x.itemName == itemName);
	}
		
	private void addItem(Item item) {
		items.Add (item);
	}
	
}

