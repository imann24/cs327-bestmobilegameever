using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InventoryManager{

	private static InventoryManager _instance;
	public static InventoryManager Instance {
		get {
			if (_instance == null) {
				_instance = new InventoryManager ();
			}
			return _instance;
		}
	}

	public bool IsHoldingItem { get { return selectedItem != null; } }
	List<InventorySlot> inventory;
	public GameObject selectedItem { get; private set; }

	InventoryManager(){
		inventory = UIManager.Instance.GetInventorySlots ();
	}

	public InventorySlot GetOpenSlot(){
		List<InventorySlot> emptySlots = inventory.FindAll (slot => slot.IsEmpty);
		if (emptySlots.Count > 0) {
			InventorySlot bestEmpty = emptySlots.OrderBy (slot => slot.transform.GetSiblingIndex ()).First ();
			return bestEmpty;
		} else {
			return null;
		}
	}
		
	//pull a reference to the object and store it in a slot.
	public bool GiveItem(string itemPath){
		
		GameObject itemToAdd = Interactable.InstantiateAsInventory (itemPath);
		if (GameManager.DEBUGGING) {
			Debug.Log ("Giving: " + itemToAdd.name + ". Item is not null?" + (itemToAdd != null).ToString());
		}
		return AddToInventory (itemToAdd);
	}

	//THIS ONE NEEDS WORK. CURRENTLY CAN'T FIND EXCEPTIONS.
	public void GiveItems(List<string> itemPaths){
		itemPaths.ForEach (delegate(string obj) {
			GiveItem (obj);
		});
	}

	//stop referencing the object
	public void TakeItem(string itemPath){
		RemoveFromInventory (itemPath);
		RemoveFromSelected (itemPath);
	}

	public void TakeItems(List<string> itemPaths){
		itemPaths.ForEach (delegate(string obj) {
			TakeItem (obj);
		});
	}

	//remove the item from the player's inventory.
	//NOTE - THIS DOES NOT PASS A REFERENCE TO THE OBJECT TO ANYTHING. IMPLEMENT DESTRUCTION OF REMOVED OBJECT SOMEWHERE
	public void RemoveFromInventory(string itemPath){
		GameObject itemToRemove = Resources.Load<GameObject>("Prefabs/" + itemPath);//load the object information
		string itemInventoryTag = itemToRemove.GetComponent<Interactable> ().InventoryTag;//get the object's inventory tag
		List<InventorySlot> filledSlots = inventory.FindAll (slot => !slot.IsEmpty);//look for inventory slots that are full
		InventorySlot itemSlot = filledSlots.Find (slot => slot.contentsTag == itemInventoryTag);//get the first one whose inventory tag matches
		if (itemSlot != null) {//if we find one, empty the slot
			itemSlot.RemoveContents ();
		}
	}

	public bool AddToInventory(GameObject itemToAdd){
		InventorySlot slotToAdd = GetOpenSlot ();
		if (slotToAdd != null) {
			slotToAdd.FillWith (itemToAdd);
			TagManager.Instance.GiveTag (slotToAdd.contentsTag);
			return true;
		} else {
			return false;
		}		
	}

	//remove the item from the player's inventory and save it as the selected item.
	public void SelectItem(InventorySlot fromSlot){
		//if the player isn't holding an item, select the item
		if (selectedItem == null) {
			selectedItem = fromSlot.RemoveContents ();
			Debug.Log ("Not holding anything... Picked up " + selectedItem.name);
			TagManager.Instance.GiveTag (selectedItem.GetComponent<Interactable> ().HoldingTag);
			UIManager.Instance.ShowSelected (selectedItem);
		} else { //otherwise try to use the selected item on the clicked item.
			fromSlot.contents.GetComponent<Interactable>().UseSelected ();
		}
	}


	//return the selected item to the player's inventory
	//NOTE - THIS METHOD DOES NOT HAVE A FAIL STATE. IT NEEDS TO BE IMPLEMENTED.
	public void ReturnSelected(){
		if (AddToInventory (selectedItem)) {
			UIManager.Instance.Deselect ();
			TagManager.Instance.TakeTag (selectedItem.GetComponent<Interactable> ().HoldingTag);
			selectedItem = null;
		}
	}

	//remove the selected item from the player.
	//This does not pass a reference to the object anywhere. Probably need to implement destruction of th object?
	public void RemoveFromSelected(string itemPath){
		GameObject itemToRemove = Resources.Load<GameObject> ("Prefabs/" + itemPath);
		string itemHoldingTag = itemToRemove.GetComponent<Interactable> ().HoldingTag;
		if (IsHoldingItem && selectedItem.GetComponent<Interactable> ().HoldingTag == itemHoldingTag) {
			UIManager.Instance.Deselect ();
			TagManager.Instance.TakeTag (selectedItem.GetComponent<Interactable> ().HoldingTag);
			selectedItem = null;
		}
	}


		
}
