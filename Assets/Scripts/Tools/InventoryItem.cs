using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public string HasTag;
	public string HoldTag;
	InventorySlot homeSlot;

	public static GameObject Create(string filePath){
		GameObject go = Instantiate (Resources.Load<GameObject> ("Prefabs/Items/" + filePath));
		if (go != null){
			go.name = Resources.Load<GameObject> ("Prefabs/Items/" + filePath).name;
			return go;
		} else {
			#if (DEBUG)
			Debug.Log(filePath + " is not a prefab inventory item. Check the Resouces/Prefabs/Items folder.");
			#endif
			return null;
		}
	} 

	/// <summary>
	/// Moves the item to the specified slot. If that slot is full, tries to move to an unoccupied slot. If there aren't any, does nothing.
	/// </summary>
	/// <param name="slot">Slot.</param>
	public void MoveTo(InventorySlot slot){
		if (!slot.Contents) {
			homeSlot = slot;
			transform.SetParent (slot.transform);
			transform.position = slot.transform.position;
			GetComponent<CanvasGroup> ().blocksRaycasts = true;
			GameManager.GiveTag (HasTag);
			GameManager.InventoryManager.Deselect ();
		} else if (GameManager.InventoryManager.FirstEmptySlot) {
			MoveTo (GameManager.InventoryManager.FirstEmptySlot);
		} else {
			#if (DEBUG)
			Debug.Log("Cannot put " + gameObject.name + " into inventory. There are no empty slots.");
			#endif
		}
	}

	public void ReturnToInventory(){
			GetComponent<Image> ().enabled = true;
			GameManager.TakeTag (HoldTag);
			MoveTo (homeSlot);
	}
		
	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData eventData)
	{
		homeSlot = GetComponentInParent<InventorySlot> ();
		transform.SetParent (GetComponentInParent<Canvas> ().transform);
		GameManager.InventoryManager.Select (gameObject);
		GameManager.TakeTag (HasTag);
		GameManager.GiveTag (HoldTag);
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}
	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		List<Interactable> dropList = eventData.hovered.Where (obj => obj.GetComponent<Interactable> () != null && obj.GetComponent<InventoryItem>() == null).Select(obj=>obj.GetComponent<Interactable>()).ToList();
		if (dropList.Count > 0) {
			if (GetComponent<Interactable> ().Debugging)
				{Debug.Log ("Using " + gameObject.name + " on " + dropList.First ().gameObject.name);}
			GetComponent<Image> ().enabled = false;
			InteractionManager.HandleUseItem (dropList.First ());
		} else {
			InteractionManager.HandleDrop (GetComponent<Interactable> ());
		}
	}

	#endregion
}
