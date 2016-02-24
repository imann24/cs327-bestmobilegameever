using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

	public GameObject contents { get; private set; }
	public string contentsTag { get { return contents.GetComponent<Interactable> ().InventoryTag; } }
	public bool IsEmpty { get { return contents == null; } }

	public GameObject RemoveContents(){
		gameObject.GetComponent<Button> ().enabled = false;
		gameObject.GetComponent<Image> ().sprite = null;
		TagManager.Instance.TakeTag (contentsTag);
		GameObject go = contents;
		contents = null;
		return go;
	}

	public void FillWith (GameObject newContents){
		gameObject.GetComponent<Button> ().enabled = true;
		gameObject.GetComponent<Image> ().sprite = 
			newContents.GetComponent<Interactable> ().InventoryImage;
		contents = newContents;
	}

	public void SelectContents(){
		InventoryManager.Instance.SelectItem (this);
	}

}
