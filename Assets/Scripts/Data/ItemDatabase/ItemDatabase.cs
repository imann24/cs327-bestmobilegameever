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

	const string ITEMSFILE = "Text/SampleItemsList";

	public List<Item> Items = new List<Item>();

	public void InitItemDatabase() {
		DataTree itemTree = DataUtil.ParseXML(ITEMSFILE);
		int i = 0;
		while (itemTree [i] != null) { // add all items to inventory
			Item item = new Item (itemTree [i]);
			Items.Add (item);
			i++;
		}
	}

	public Item GetItem(string itemName) {
		return Items.Find(x => x.ItemName == itemName);
	}

	public void AddItem(string itemName) {
		GetItem (itemName).AddItem();
	}

	public void RemoveItem(string itemName) {
		GetItem (itemName).RemoveItem();
	}
	
}

