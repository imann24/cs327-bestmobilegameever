/*
 * Author: Isaiah Mann
 * Description: A report on the status of the inventory
 */

using System.Collections.Generic;
public class InventoryReport {

	const string ITEM_COLLECTED_KEY = "Collected Item";
	const string ITEM_DESTROYED_KEY = "Destroyed Item";
	const string CURRENT_INVENTORY_KEY = "Current Inventory";

	Dictionary<string, object> details = new Dictionary<string, object>();

	public InventoryReport (InventoryItem [] inventoryItems) {
		string [] itemNames = new string[inventoryItems.Length];

		for (int i = 0; i < itemNames.Length; i++) {
			itemNames[i] = inventoryItems[i].Name;
		}

		overwriteDetailEntry(
			CURRENT_INVENTORY_KEY,
			ArrayUtil.ToString(itemNames)
		);
	}

	// Overwrites the current item if any
	public void AddItemCollected (string itemName) {
		overwriteDetailEntry(
			ITEM_COLLECTED_KEY,
			itemName
		);
	}

	// Overwrites the current item if any
	public void AddItemDestroyed (string itemName) {
		overwriteDetailEntry(
			ITEM_DESTROYED_KEY,
			itemName
		);
	}

	void overwriteDetailEntry (string key, object value) {
		if (details.ContainsKey(key)) {
			details[key] = value;
		} else {
			details.Add(key, value);
		}
	}

	InventoryItemSummary getItemSummary (InventoryItem item) {
		return new InventoryItemSummary(item);
	}

	public Dictionary<string, object> Get () {
		return details;
	}

	public override string ToString () {
		return string.Format ("[InventoryReport] {0}", DictionaryUtil.ToString(details));
	}

}