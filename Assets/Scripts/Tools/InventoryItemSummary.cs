/*
 * Author: Isaiah Mann
 * Description: Returns a text report on an inventory item
 */
using UnityEngine;
using System.Collections;

public class InventoryItemSummary {
	public string Name;
	public string HasTag;
	public string HoldTag;

	public InventoryItemSummary (InventoryItem item) {
		this.Name = item.gameObject.name;
		this.HasTag = item.HasTag;
		this.HoldTag = item.HoldTag;
	}

	public string Get () {
		return string.Format ("[InventoryItemSummary] HasTag:{1}, HoldTag:{2}", Name, HasTag, HoldTag);
	}

	public override string ToString () {
		return string.Format ("[InventoryItemSummary] Name:{0}, HasTag:{1}, HoldTag:{2}", Name, HasTag, HoldTag);
	}
}
