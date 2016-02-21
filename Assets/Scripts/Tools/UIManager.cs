using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class UIManager : MonoBehaviour {

	public static UIManager Instance { get; private set; }

	public bool IsInventoryShowing { get; private set; }
	public bool IsOptionsShowing { get; private set; }
	public bool IsSettingsShowing { get; private set; }
	public bool IsInteractionShowing { get; private set; }

	void Awake(){//use the singleton pattern to avoid multiple interfaces.
		if (Instance == null) {
			Instance = this;
		} else if (this != Instance) {
			Destroy (gameObject);
			if (GameManager.DEBUGGING) {
				Debug.Log ("Too Many UIManagers!");
			}
		}
	}

	void Start(){
		ShowInventory ();
	}

	public void ShowInventory(){
		IsInventoryShowing = true;
		transform.FindChild ("InventoryPanel").gameObject.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, -5);
		transform.FindChild ("InventoryPanel/Toggle/Text").gameObject.GetComponent<Text> ().text = "Hide";
	}

	public void HideInventory(){
		IsInventoryShowing = false;
		transform.FindChild ("InventoryPanel").gameObject.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, -90);
		transform.FindChild ("InventoryPanel/Toggle/Text").gameObject.GetComponent<Text> ().text = "Show";
	}

	public void ToggleInventory(){
		if (IsInventoryShowing) {
			HideInventory ();
		} else {
			ShowInventory ();
		}
	}

	public List<InventorySlot> GetInventorySlots(){
		return transform.FindChild ("InventoryPanel").gameObject.GetComponentsInChildren<InventorySlot> ().ToList ();
	}

	public void ShowSelected(GameObject selected){
		transform.FindChild ("SelectionPanel/SelectionImage").gameObject.GetComponent<Image> ().sprite = selected.GetComponent<Interactable> ().InventoryImage;
	}

	public void Deselect(){
		transform.FindChild ("SelectionPanel/SelectionImage").gameObject.GetComponent<Image> ().sprite = null;
	}

	public void ShowOptions(){
		IsOptionsShowing = true;
		transform.FindChild ("OptionsMenu").gameObject.SetActive (true);
	}

	public void CloseOptions(){
		IsOptionsShowing = false;
		transform.FindChild ("OptionsMenu").gameObject.SetActive (false);
	}

	public void ShowSettings(){
		IsSettingsShowing = true;
		transform.FindChild ("SettingsPanel").gameObject.SetActive (true);
		CloseOptions ();
	}

	public void CloseSettings(){
		IsSettingsShowing = false;
		transform.FindChild ("SettingsPanel").gameObject.SetActive (false);
		ShowOptions ();
	}

	public void ShowInteractionPanel(){
		IsInteractionShowing = true;
		transform.FindChild ("InteractionPanel").gameObject.SetActive (true);
		if (GameManager.DEBUGGING) {
			Debug.Log ("showing interaction panel");
		}
	}
		
	public void ChangeInteractionImage(Sprite newImage){
		transform.FindChild ("InteractionPanel/InteractionImagePanel/InteractionImage").gameObject.GetComponent<Image> ().sprite = newImage;
	}

	public void CloseInteractionPanel(){
		IsInteractionShowing = false;
		GameObject interactionTextPanel = transform.FindChild ("InteractionPanel/InteractionTextPanel").gameObject;
		List<Transform> interactionTransforms = interactionTextPanel.GetComponentsInChildren<Transform> ().ToList ();
		foreach (Transform t in interactionTransforms) {
			if (t != interactionTextPanel.transform) {
				Destroy (t.gameObject);
			}
		}
		transform.FindChild ("InteractionPanel").gameObject.SetActive (false);
		if (GameManager.DEBUGGING) {
			Debug.Log ("closing interaction panel");
		}
	}
}
