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
			DontDestroyOnLoad (gameObject);
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
	/// <summary>
	/// Shows the inventory.
	/// </summary>
	void ShowInventory(){
		IsInventoryShowing = true;
		transform.FindChild ("InventoryPanel").gameObject.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, -5);
		transform.FindChild ("InventoryPanel/Toggle/Text").gameObject.GetComponent<Text> ().text = "Hide";
	}
	/// <summary>
	/// Hides the inventory.
	/// </summary>
	void HideInventory(){
		IsInventoryShowing = false;
		transform.FindChild ("InventoryPanel").gameObject.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, -90);
		transform.FindChild ("InventoryPanel/Toggle/Text").gameObject.GetComponent<Text> ().text = "Show";
	}
	/// <summary>
	/// Toggles the inventory display.
	/// </summary>
	public void ToggleInventory(){
		if (IsInventoryShowing) {
			HideInventory ();
		} else {
			ShowInventory ();
		}
	}

	/// <summary>
	/// Gets the inventory slots.
	/// </summary>
	/// <returns>A list of inventory slots in the inventory panel.</returns>
	public List<InventorySlot> GetInventorySlots(){
		//return transform.FindChild ("InventoryPanel").gameObject.GetComponentsInChildren<InventorySlot> ().ToList ();
		Debug.Log("Slots:" + GetComponentsInChildren<InventorySlot>(true).ToList().Count);
		return GetComponentsInChildren<InventorySlot> (true).ToList();
	}

	/// <summary>
	/// Shows the selected item in the selection display panel.
	/// </summary>
	/// <param name="selected">Selected.</param>
	public void ShowSelected(GameObject selected){
		transform.FindChild ("SelectionPanel/SelectionImage").gameObject.GetComponent<Image> ().sprite = selected.GetComponent<Interactable> ().InventoryImage;
	}

	/// <summary>
	/// Hides the image of the selected item.
	/// </summary>
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
		DisableTapToContinue ();
		CloseInteractionImages ();
		transform.FindChild ("InteractionPanel").gameObject.SetActive (false);
	}

	public void ShowInteractionImageLeft(Sprite newImage){
		GameObject interactionImagePanel = transform.FindChild ("InteractionPanel/InteractionImagePanelLeft").gameObject;
		interactionImagePanel.SetActive (true);
		interactionImagePanel.transform.FindChild ("InteractionImage").GetComponent<Image> ().sprite = newImage;
	}

	public void ShowInteractionImageRight(Sprite newImage){
		GameObject interactionImagePanel = transform.FindChild ("InteractionPanel/InteractionImagePanelRight").gameObject;
		interactionImagePanel.SetActive (true);
		interactionImagePanel.transform.FindChild ("InteractionImage").GetComponent<Image> ().sprite = newImage;
	}

	public void CloseInteractionImages(){
		GameObject panelLeft = transform.FindChild ("InteractionPanel/InteractionImagePanelLeft").gameObject;
		GameObject panelRight = transform.FindChild ("InteractionPanel/InteractionImagePanelRight").gameObject;
		panelLeft.transform.FindChild ("InteractionImage").GetComponent<Image> ().sprite = null;
		panelRight.transform.FindChild ("InteractionImage").GetComponent<Image> ().sprite = null;
		panelLeft.SetActive (false);
		panelRight.SetActive (false);
	}

	public void GenerateOption(Interactable interactor, Interaction interaction){
		if (interactor.Debugging) {
			Debug.Log ("Showing Interaction Panel");
		}
		ShowInteractionPanel ();
		Transform panelTransform = transform.FindChild ("InteractionPanel/InteractionTextPanel");
		GameObject newButton = Instantiate(Resources.Load<GameObject>("Prefabs/InteractionButton"));
		newButton.GetComponentInChildren<Text> ().text = interaction.iText;
		newButton.GetComponent<InteractionButton> ().buttonInteraction = interaction;
		newButton.GetComponent<InteractionButton> ().buttonInteractor = interactor;
		newButton.transform.SetParent(panelTransform);
	}

	public void GenerateMonologue(Interactable interactor, Interaction interaction){
		if (interactor.Debugging) {
			Debug.Log ("Showing Interaction Panel");
		}
		ShowInteractionPanel ();
		Transform panelTransform = transform.FindChild ("InteractionPanel/InteractionTextPanel");
		GameObject newMonologue = Instantiate(Resources.Load<GameObject>("Prefabs/MonologuePanel"));
		newMonologue.GetComponentInChildren<Text> ().text = interaction.iText;
		newMonologue.transform.SetParent (panelTransform);
	}

	public void EnableTapToContinue(Interactable interactor, Interaction interaction){
		transform.FindChild ("InteractionPanel/InteractionTextPanel").gameObject.GetComponentInChildren<Button>().enabled = false;
		GameObject ttcButton = transform.FindChild ("TapToContinueButton").gameObject;
		ttcButton.SetActive (true);
		ttcButton.GetComponent<InteractionButton> ().buttonInteraction = interaction;
		ttcButton.GetComponent<InteractionButton> ().buttonInteractor = interactor;
	}

	public void DisableTapToContinue(){
		transform.FindChild ("TapToContinueButton").gameObject.SetActive (false);
	}
}
