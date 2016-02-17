/*
 * Author: Donna Pan
 * Description: A class for storing an item
 */

using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	const int SINGLE = 0;
	const int MERGE = 1;

	public string ItemName = "";
	public string ItemDescription = "";
	public int ItemCount = 0;
	public string[] Icons;
	public int ItemType; //merged or non-merged
	//enum for item type
	//[System.NonSerialized] // Texture2D's are non-serializable

	public Item(DataNode item) {
		ItemName = item[0].Value;
		ItemDescription = item[1].Value;
		if(item[2].Value.Equals("yes")) {
			ItemType = MERGE;
		} else {
			ItemType = SINGLE;
		}
		int i = 3;
		while (item [i] != null) { // read all sprite names
			Icons [i] = "Visual/"+item [i];
		}
	}

	public void InitializeItem(DataNode item) {
		ItemName = item[0].Value;
		ItemDescription = item[1].Value;
		if(item[2].Value.Equals("true")) {
			ItemType = MERGE;
		} else {
			ItemType = SINGLE;
		}
		int i = 3;
		while (item [i] != null) {
			Icons[i] = "Visual/"+item [i];
		}
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

