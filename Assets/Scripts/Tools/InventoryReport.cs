/*
 * Author: Isaiah Mann
 * Description: A report on the status of the inventory
 */

using System.Collections.Generic;
public class InventoryReport {

	Dictionary<string, object> items = new Dictionary<string, object>();

	public InventoryReport (InventoryItem [] inventoryItems) {
		foreach (InventoryItem item in inventoryItems) {
			this.items.Add(item.Name, getItemSummary(item));
		}
	}

	InventoryItemSummary getItemSummary (InventoryItem item) {
		return new InventoryItemSummary(item);
	}

	public Dictionary<string, object> Get () {
		return items;
	}

}