using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InvSlot : MonoBehaviour {

	public InvItem contents { get; private set; }

	public bool isEmpty { get { return contents == null; } }

	public void AddItem(InvItem filler){
		contents = filler;
		gameObject.GetComponent<Image> ().sprite = filler.itemSprite;
		Button bo = gameObject.AddComponent<Button> ();
		bo.onClick.AddListener (() => {Inventory.Instance.SelectItem(contents);});
	}

	public void DropItem(){
		contents = null;
		gameObject.GetComponent<Image> ().sprite = null;
		Destroy (gameObject.GetComponent<Button> ());
	}
		
	public void SelectItem(){
		gameObject.GetComponent<Image> ().sprite = null;
		gameObject.GetComponent<Button> ().enabled = false;
	}

	public void ReplaceItem(){
		gameObject.GetComponent<Image> ().sprite = contents.itemSprite;
		gameObject.GetComponent<Button> ().enabled = true;
	}
}
