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

	public List<Item> Items = new List<Item>();

	// stub for now; should read item information from the controller(?) in a certain format
	// and addItem to the list of items
	// But we should just do it via the Unity interface...
	public void InitItemDatabase(string filePath) {
		DataTree ItemsTree = XMLReader.ReadXMLAsDataTree(XMLReader.Read (filePath));
	}


	public Item GetItem(string itemName) {
		return Items.Find(x => x.ItemName == itemName);
	}
		
	private void _addItem(Item item) {
		Items.Add (item);
	}
	
}

