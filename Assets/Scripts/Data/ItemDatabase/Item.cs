/*
 * Author: Donna Pan
 * Description: A class for storing an item
 */

using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item : MonoBehaviour {

	public string ItemName = "";
	public string ItemDescription = "";
	public int ItemCount = 0;
	//enum for item type

	[System.NonSerialized] // Texture2D's are non-serializable
	Texture2D Icon;

	public Item(DataNode item) {
		ItemName = item[0].Value;
		ItemDescription = item[1].Value;
		Icon = null;
	}

	public void InitializeItem(DataNode item) {
		ItemName = item[0].Value;
		ItemDescription = item[1].Value;
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

