using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IBeginDragHandler{

	[SerializeField]
	private GameObject ToggleButton = null;
	[SerializeField]
	private Sprite ShowButton = null;
	[SerializeField]
	private Sprite HideButton = null;
	private Vector2 dragAnchor;



	public GameObject Selected { get; private set; }
	public bool PanelShowing { get; private set; }

	private InventorySlot[] slots { get { return GetComponentsInChildren<InventorySlot> (); } }
	public InventorySlot FirstEmptySlot { get { return slots.FirstOrDefault (slot => slot.Contents == null); } }


	/// <summary>
	/// Gives the item.
	/// </summary>
	/// <returns><c>true</c>, if item was given, <c>false</c> otherwise.</returns>

	public bool GiveItem(string item){
		GameManager.InventoryManager.gameObject.SetActive (true);
		if (FirstEmptySlot) {
			Show ();
			GameObject itemToGive = InventoryItem.Create (item);
			if (itemToGive) {
				this.gameObject.SetActive (true);
				itemToGive.GetComponent<InventoryItem> ().MoveTo (FirstEmptySlot);
				itemToGive.GetComponent<Image> ().preserveAspect = true; 
				return true;
			} else {
				#if (DEBUG)
				Debug.Log("There is no inventory item called " + item + " in Resources/Prefabs/Items");
				#endif
				return false;
			}
		} else {
			#if (DEBUG)
			Debug.Log("No empty slots for " + item);
			#endif
			return false;
		}
	}

	/// <summary>
	/// Gives each item in the list.
	/// </summary>
	/// <returns><c>true</c>, if each item in the list was given, <c>false</c> otherwise.</returns>
	/// <param name="items">Items.</param>
	public bool GiveItemList(List<string> items){
		List<string> successes = items.FindAll (i => GiveItem (i));
		#if (DEBUG)
		Debug.Log("Successfully given: " + string.Join(" ", successes.ToArray()));
		#endif
		return successes == items;
	}

	/// <summary>
	/// Takes the item.
	/// </summary>
	/// <returns><c>true</c>, if item was taken, <c>false</c> otherwise.</returns>
	/// <param name="item">Item.</param>
	public bool TakeItem(string item){
		GameManager.InventoryManager.ReturnSelected ();
		InventoryItem itemToTake = GetComponentsInChildren<InventoryItem> ().FirstOrDefault (i => i.gameObject.name == item);
		if (itemToTake != null) {
			GameManager.TakeTag (itemToTake.GetComponent<InventoryItem> ().HasTag);
			Destroy (itemToTake.gameObject);
			return true;
		} else {
			#if (DEBUG)
			Debug.Log("You don't have an item called " + item + ", are you supposed to?");
			#endif
			return false;
		}
	}

	/// <summary>
	/// Takes each item in the list.
	/// </summary>
	/// <returns><c>true</c>, if each item in the list was taken, <c>false</c> otherwise.</returns>
	/// <param name="items">Items.</param>
	public bool TakeItemList(List<string> items){
		List<string> successes = items.FindAll (i => TakeItem (i));
		#if (DEBUG)
		Debug.Log("Items successfully taken " + string.Join(" ", items.ToArray()));
		#endif
		return false;
	}

	/// <summary>
	/// Returns the selected item to the inventory.
	/// </summary>
	public void ReturnSelected(){
		if (Selected != null) {
			Selected.GetComponent<InventoryItem> ().ReturnToInventory ();
		}
	}

	public void Deselect(){
		Selected = null;
	}

	/// <summary>
	/// Select the specified item.
	/// </summary>
	/// <param name="item">Item.</param>
	public void Select(GameObject item){
		Selected = item;
	}

	/// <summary>
	/// Show the inventory panel.
	/// </summary>
	public void Show(){
		PanelShowing = true;
		ToggleButton.GetComponent<Image> ().sprite = HideButton;
		StartCoroutine ("ChangeHeight", Vector2.zero);
	}

	/// <summary>
	/// Hide the inventory panel.
	/// </summary>
	public void Hide(){
		PanelShowing = false;
		StartCoroutine("ChangeHeight", new Vector2(0,-105));
		ToggleButton.GetComponent<Image> ().sprite = ShowButton;
	}

	/// <summary>
	/// Toggle between showing and hidden.
	/// </summary>
	public void Toggle(){
		if (PanelShowing) {
			Hide ();
		} else {
			Show ();
		}
	}

	#region IPointerEnterHandler implementation

	public void OnPointerEnter (PointerEventData eventData)
	{
		if (Selected != null && !PanelShowing)
			Show ();
	}

	#endregion

	#region IPointerExitHandler implementation

	public void OnPointerExit (PointerEventData eventData)
	{
		if (Selected != null && PanelShowing)
			Hide ();
	}

	#endregion

	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		Debug.Log ("started drag");
		dragAnchor = Input.mousePosition;
		Debug.Log (dragAnchor.y);
		//Toggle ();
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		if (dragAnchor.y > Input.mousePosition.y && PanelShowing) {
			dragAnchor = Input.mousePosition;
			Debug.Log (dragAnchor.y);
			Hide ();
		}
		if (dragAnchor.y < Input.mousePosition.y && !PanelShowing) {
			dragAnchor = Input.mousePosition;
			Debug.Log (dragAnchor.y);
			Show ();
		}
	}

	#endregion

	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
	{
	}

	#endregion

	/// <summary>
	/// Changes the height over time.
	/// </summary>
	/// <param name="target">Target.</param>
	IEnumerator ChangeHeight(Vector2 target){
		float changeDuration = 0.5f;
		Vector2 start = GetComponent<RectTransform> ().anchoredPosition;
		for (float t = 0; t < changeDuration; t += Time.deltaTime) {
			GetComponent<RectTransform> ().anchoredPosition = Vector2.Lerp (start, target, t / changeDuration);
			yield return null;
		}
		GetComponent<RectTransform> ().anchoredPosition = target;
	}
}
