using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Inventory {

	//There should only ever be one Inventory in the game, so I implemented a singleton pattern.
	private static Inventory instance;
	public static Inventory Instance { 
		get {
			if (instance == null) {
				instance = new Inventory ();
			}
			return instance;
		}
	}


	public InvPanel panel { get; private set; }
	private InvSlot[] slots;
	public InvItem selectedItem { get; private set; }

	private Inventory() {
		panel = GameObject.Find ("inventory_panel").GetComponent<InvPanel>();
		slots = panel.transform.GetComponentsInChildren<InvSlot> ();
	}

	public void AddItem(InvItem item){
		List<InvSlot> emptySlots = new List<InvSlot> (slots).FindAll (slot => slot.isEmpty);
		if (emptySlots.Count > 0) {
			panel.Show ();
			InvSlot emptySlot = emptySlots.OrderBy (slot => slot.transform.GetSiblingIndex ()).ToList ().First ();
			emptySlot.AddItem (item);
			item.PickUp (emptySlot);
			Debug.Log (item.itemName + " added to slot " + emptySlot.transform.GetSiblingIndex().ToString());
		} else {
			Debug.Log ("Inventory Full.");
		}
	}

	public void SelectItem(InvItem item){
		selectedItem = item;
		item.itemSlot.SelectItem ();
		panel.Hide ();
	}

	public void DropItem(){
		selectedItem.itemSlot.DropItem ();
		selectedItem.Drop ();
		selectedItem = null;
	}

	public void ReplaceItem(){
		panel.Show ();
	}
}