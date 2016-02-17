using UnityEngine;
using System.Collections;

public class InvItem : MonoBehaviour {

	public string itemName;
	public Texture2D itemTexture;
	public Sprite itemSprite { get { return Sprite.Create (itemTexture, new Rect (0, 0, itemTexture.width, itemTexture.height), Vector2.zero); } }
	public InvSlot itemSlot { get; private set; }
	// Use this for initialization

	void OnMouseUpAsButton(){
		if (Inventory.Instance.selectedItem == null) { //If you click on an item in the screen and don't currently have one selected...
			Inventory.Instance.AddItem (this);     //add it to inventory
		}
	}

	public void PickUp(InvSlot slot){
		gameObject.GetComponent<SpriteRenderer> ().enabled = false;//stop showing it in the screen
		gameObject.GetComponent<Collider> ().enabled = false;//stop being able to click on it
		itemSlot = slot;//record where we put it.
	}

	public void Drop(){
		this.transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition) + Vector3.forward * 10;
		Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition).ToString());
		gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		gameObject.GetComponent<Collider> ().enabled = true;
		itemSlot = null;
	}
}