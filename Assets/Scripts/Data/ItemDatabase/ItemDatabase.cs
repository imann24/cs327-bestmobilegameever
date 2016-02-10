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

	const string ITEMSFILE = "Text/SampleTextFile";

	public List<Item> Items = new List<Item>();

	// stub for now; should read item information from the controller(?) in a certain format
	// and addItem to the list of items
	// But we should just do it via the Unity interface...
	public void InitItemDatabase(string filePath) {
		DataTree itemTree = DataUtil.ParseXML(ITEMSFILE);
		int i = 0;
		while (itemTree [i] != null) { // add all items to inventory
			Item item = new Item (itemTree [i]);
			Items.Add (item);
		}
	}

	public Item GetItem(string itemName) {
		return Items.Find(x => x.ItemName == itemName);
	}
	
}

