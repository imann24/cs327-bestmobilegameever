using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Interactable : MonoBehaviour, IPointerClickHandler{

	//The instructable's list of interactions. This is automatically loaded when the object fires Start().
	public List<Interaction> Interactions { get; private set; }
	[Tooltip("The name of the xml file where the interactions are defined, without the '.xml' extension. This file should be in the Resources/Text folder.")]
	public string InteractionsXmlPath;
	[Tooltip("The image that will display when the interactable is in the inventory. If it will never be in inventory, this can be left blank.")]
	public Sprite InventoryImage;
	[Tooltip("The tag the player has while the interactable is in the inventory. If it will never be in inventory, this can be left blank.")]
	public string InventoryTag; 
	[Tooltip("The tag the player has while the player is HOLDING the interactable. If it will never be in inventory, this can be left blank.")]
	public string HoldingTag;
	[Tooltip("Show debugging messages in the console.")]
	public bool Debugging;

	// Use this for initialization
	void Awake(){
		Interactions = InteractionList.Load ("Text/" + InteractionsXmlPath);
		if (Debugging && Interactions.Count == 0) {
			Debug.Log ("The interactions list for " + gameObject.name + " is empty. Check the Resources/Text folder for " + InteractionsXmlPath + ".");
		}
	}

	void Start(){
		//*
		if (Debugging) {
			Debug.Log (Interactions.Count);
			foreach (Interaction i in Interactions) {
				Debug.Log (i.ToString ());
			}
		}//*/
	}

	/// <summary>
	/// Instantiate the prefab indicated by 'path' as an inventory object. The prefab should be stored in the "Resources/Prefabs" folder.
	/// </summary>
	/// <returns>The as inventory.</returns>
	/// <param name="path">The path to the prefab. The prefab should be stored in the "Resources/Prefabs" folder. </param>
	public static GameObject InstantiateAsInventory(string path){
		GameObject go = Instantiate (Resources.Load<GameObject> ("Prefabs/" + path)) as GameObject;
		go.name = Resources.Load<GameObject> ("Prefabs/" + path).name;
		if (go != null) {
			Collider col = go.GetComponent<Collider> ();
			if(col != null){
				col.enabled = false;
			}
			Renderer rnd = go.GetComponent<Renderer> ();
			if (rnd != null) {
				rnd.enabled = false;
			}
			Rigidbody rb = go.GetComponent<Rigidbody> ();
			if (rb != null) {
				rb.detectCollisions = false;
			}
			return go;
		} else {
			if (GameManager.DEBUGGING) {
				Debug.Log (path + "is not a prefab. Check the Resources/Prefabs folder.");
			}
			return null;
		}
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		if (!UIManager.Instance.IsInteractionShowing) {
			if (InventoryManager.Instance.IsHoldingItem) {
				InteractionManager.HandleUseItem (this);
			} else {
				InteractionManager.HandleOnClick (this);
			}
		}
	}

	/// <summary>
	/// Despawn this instance.
	/// </summary>
	public void Despawn(){
		Destroy (gameObject);
	}
	/// <summary>
	/// Try to use the currently selected item on this interactable. If there are no valid interactions, use the "DefaultCannotUse" interacted in the selected item's interaction list. If it doesn't have one, return the item to inventory.
	/// </summary>
	public void UseSelected(){
		if(!UIManager.Instance.IsInteractionShowing){
		Interactable selected = InventoryManager.Instance.selectedItem.GetComponent<Interactable> ();
		List<Interaction> validItemsForTarget = Interactions.FindAll (i => (i.iType == InteractionType.UseItem) && (i.IsValid));// && (i.iAllTags.Contains (selected.HoldingTag)));
		Debug.Log ("UseSelected() validItemsForTarget: " + string.Join (" ", validItemsForTarget.ConvertAll (new System.Converter<Interaction, string> (i => i.iName)).ToArray ()));
			if (validItemsForTarget.Count > 0) {
				InteractionManager.HandleInteractionList (this, validItemsForTarget);
			} else {
				if (Debugging) {
					Debug.Log ("Used " + selected.gameObject.name + " on " + gameObject.name + ". No interactions were found. The player tags were: " + string.Join (", ", TagManager.Instance.PlayerTags.ToArray ()));
				}
				Interaction error = selected.Interactions.Find (i => i.iName == "DefaultCannotUse");
				if (error != null) {
					InteractionManager.HandleInteractionSingle (this, error);
				} else {
					if (Debugging) {
						Debug.Log ("There is no 'DefaultCannotUse' interaction for " + selected.gameObject.name + ".");
					}
					InventoryManager.Instance.ReturnSelected ();
				}
			}
		}
	}

	/// <summary>
	/// Handle any interactions that stem from hitting this interactable with an orange.
	/// </summary>
	public void OnOrange(){
		List<Interaction> subList = Interactions.FindAll (interaction => interaction.iType == InteractionType.Orange);
		InteractionManager.HandleInteractionList (this, subList);
		if (Debugging) {
			Debug.Log ("Threw an orange at " + gameObject.name + ". No interactions were found. The player tags were: " + string.Join(", ", TagManager.Instance.PlayerTags.ToArray()));
		}
	}

	/// <summary>
	/// Tell the SpecialActions class (or sub-class) on the game object to run the code associated with each string in actionsToDo.
	/// </summary>
	/// <param name="actionsToDo">Actions to do.</param>
	public void DoSpecialActions(List<string> actionsToDo){
		if (gameObject.GetComponent<SpecialActions> () != null) {
			gameObject.GetComponent<SpecialActions> ().DoSpecialActions (actionsToDo);
		} else {
			if (Debugging && actionsToDo.Count > 0) {
				Debug.Log (gameObject.name + " is missing a DoSpecialActions script and needs one.");
			}
		}
	}
}
