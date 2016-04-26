using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler{

	public GameObject Contents { get { return (transform.childCount > 0) ? transform.GetChild (0).gameObject : null; } }

	public void OnDrop (PointerEventData eventData)
	{
		if (Contents != null) {
			#if (DEBUG)
			Debug.Log("Using " + GameManager.InventoryManager.Selected.name + " on " + Contents.name);
			#endif
			InteractionManager.HandleUseItem (Contents.GetComponent<Interactable>());
		} else {
			#if (DEBUG)
			Debug.Log("Moving " + GameManager.InventoryManager.Selected.name);
			#endif

			// Removes the holding tag so the game no longer assumes you're holding the item
			GameManager.TakeTag (GameManager.InventoryManager.Selected.GetComponent<InventoryItem> ().HoldTag);

			GameManager.InventoryManager.Selected.GetComponent<InventoryItem> ().MoveTo (this);
		}
	}

	public bool HasInventoryItem () {
		return GetComponentInChildren<InventoryItem>() != null;
	}

	public InventoryItem GetInventoryItem () {
		return GetComponentInChildren<InventoryItem>();
	}
}
