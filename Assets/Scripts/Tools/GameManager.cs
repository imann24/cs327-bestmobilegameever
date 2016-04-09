using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {

	private static GameManager _instance = null;
	public static List<string> PlayerTags { get; private set; }

	[SerializeField]
	GameObject _playerCharacter = null;
	public static GameObject PlayerCharacter { get { return GameManager._instance._playerCharacter; } }

	[SerializeField]
	private GameObject _uiManager = null;
	public static UIManager UIManager { get { return _instance._uiManager.GetComponent<UIManager>(); }}

	[SerializeField]
	private GameObject _inventoryManager = null;
	public static InventoryManager InventoryManager { get { return _instance._inventoryManager.GetComponent<InventoryManager>(); } }

	[SerializeField]
	private GameObject _interactionManager = null;
	public static InteractionManager InteractionManager { get { return _instance._interactionManager.GetComponent<InteractionManager> (); } }

	void Awake(){
		if (_instance == null) {
			_instance = this;
			PlayerTags = new List<string> ();
			DontDestroyOnLoad (gameObject);
		} else if (this != _instance) {
			Destroy (gameObject);
		}
	}

	void Start(){
		PlayerTags.Add ("intro");
		InventoryManager.Hide();
	}

    public static bool InteractionActive {
        get {
            GameObject InteractionPanel = GameObject.Find("InteractionTextPanel");
            if (InteractionPanel != null) { return true; }
            else { return false; }
        }
    }


	#region TAG MANAGEMENT FUNCTIONS
	public static void GiveTags(List<string> tags){
		PlayerTags = PlayerTags.Union (tags).Distinct ().ToList ();
	}

	public static void GiveTag(string tag){
		GiveTags (new List<string> { tag });
	}

	public static void TakeTags(List<string> tags){
		PlayerTags = PlayerTags.Distinct ().Except (tags).ToList ();
	}

	public static void TakeTag(string tag){
		TakeTags (new List<string> { tag });
	}

	public static bool HasTag(string tag){
		return PlayerTags.Contains (tag);
	}

	public static bool HasAllTags(List<string> tags){
		return tags.TrueForAll (tag => HasTag (tag));
		//TrueForAll returns true if the list has no elements.
	}

	public static bool HasAnyTags(List<string> tags){
		if (tags.Count > 0) {
			return tags.FindAll (tag => HasTag (tag)).Count > 0;
		} else {
			return true;
		}
	}

	public static bool HasNoneTags(List<string> tags){
		if (tags.Count > 0) {
			List<string> badTags = tags.FindAll (tag => HasTag (tag));
			if (badTags.Count > 0) {
				#if (DEBUG)
				Debug.Log ("Checking NONE tags. Conflicting tags: " + string.Join (" ", badTags.ToArray ()));
				#endif
				return false;
			} else {
				return true;
			}
		} else {
			return true;
		}
	}
	#endregion
}
