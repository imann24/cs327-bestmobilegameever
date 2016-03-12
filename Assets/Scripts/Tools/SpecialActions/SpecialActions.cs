using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SpecialActions : MonoBehaviour {
    private Dictionary<string, SpecialActions> actionScripts = new Dictionary<string, SpecialActions>();

    // Possible Extended Special Actions & Parameters
    // Always HideInInspector so they don't show in scripts that inherit them
    // The SpecialActionsEditor handles what is shown
    [HideInInspector]
    public string ActionTag = null, Sound;
    [HideInInspector]
    public bool UseExtended, ChangesSprite, MovesObject, CreatesObject, PlaysSound;
    [HideInInspector]
    public Sprite NewSprite;
    [HideInInspector]
    public GameObject ObjectToMove, ObjectToCreate, CreateAtWaypoint, MoveToWaypoint;
    [HideInInspector]
    public Vector3 MoveToPosition, CreateAtPosition;
    [HideInInspector]
    public float SoundDelay = 0;

    public void Awake() {
        SpecialActions[] scripts = GetComponents<SpecialActions>();
        foreach (SpecialActions script in scripts) {
            if (script.ActionTag != null) { actionScripts.Add(script.ActionTag, script); }
        }
    }

    private void DoExtendedAction() {
        if (ChangesSprite) { ChangeSprite(NewSprite); }
        if (MovesObject) { MoveObject(ObjectToMove, MoveToPosition); }
        if (CreatesObject) { CreateObject(ObjectToCreate, CreateAtPosition); }
        if (PlaysSound) {
            if (SoundDelay == 0) { PlaySound(Sound); }
            else { Invoke("PlaySound", SoundDelay); }
        }
    }

    #region useful functions
    public void ChangeSprite(Sprite sprite) {
        if (GetComponentInChildren<SpriteRenderer>() != null) { GetComponentInChildren<SpriteRenderer>().sprite = sprite; }
        else if (GetComponent<Image>() != null) { GetComponent<Image>().sprite = sprite; }
        else if (GetComponent<SpriteRenderer>() != null) { GetComponent<SpriteRenderer>().sprite = sprite; }
        else if (gameObject.GetComponent<Interactable>().Debugging) { Debug.Log("No valid sprite or image."); }
    }
    
    public void PlaySound(string sound = null) {
        if (sound == null) { EventController.Event(Sound); }
        else { EventController.Event(sound); }
    }

    public void MoveObject(GameObject obj, Vector2 newPos) { obj.transform.position = new Vector3(newPos.x, newPos.y, obj.transform.position.z); }

    public void CreateObject(GameObject obj, Vector2 pos) {
        GameObject newObject = (GameObject)Instantiate(obj, new Vector3(pos.x, pos.y, gameObject.transform.position.z), Quaternion.identity);
        newObject.name = obj.name;
    }

    public void NextInteraction(string name, Interactable interactor = null)
    {
        if (interactor == null) { interactor = gameObject.GetComponent<Interactable>(); }
        List<Interaction> iList = interactor.Interactions.FindAll(i => (i.iName == name) && (i.iType == InteractionType.Derivative));
        if (gameObject.GetComponent<Interactable>().Debugging) { Debug.Log("Attempting to run interaction with name '" + name + "' that belongs to " + interactor); }
        InteractionManager.HandleInteractionList(interactor, iList);
    }
    #endregion

    #region defaults
    public void DoSpecialActions(List<string> actionList) {
		bool destroy = false;
		foreach (string action in actionList) {
			switch (action) {
			case "ReturnSelected":
				GameManager.InventoryManager.ReturnSelected();
				break;
			case "Destroy":
				destroy = true;
				break;
			case "ComeHere":
				//TODO: Implement Joel's movement system
				//GameManager.Instance.playerCharacter.GetComponent<Movement> ().MoveTo (transform.position);
				break;
            case "FadeIn":
                ScreenFader.FadeIn();
                break;
            case "FadeOut":
                ScreenFader.FadeOut();
                break;
			default:
                if (actionScripts.ContainsKey(action)) { actionScripts[action].DoExtendedAction(); }
                DoSpecialAction (action);
				break;
			}
		}
		if (destroy) {
			Destroy (gameObject);
		}
    }

    //override this function in subclasses for specific actions.
    public virtual void DoSpecialAction(string actionTag) {
        if (gameObject.GetComponent<Interactable>().Debugging) {
            Debug.Log(actionTag + " is not defined. Using default SpecialActions script.");
        }
    }
    #endregion
}

#if UNITY_EDITOR
#region editor
[CustomEditor(typeof(SpecialActions))]
public class SpecialActionsEditor : Editor {
    public override void OnInspectorGUI() {
        var thisScript = target as SpecialActions;
        if (thisScript) {
            thisScript.UseExtended = GUILayout.Toggle(thisScript.UseExtended, "Use Extended Special Actions");
            if (thisScript.UseExtended) {
                thisScript.ActionTag = EditorGUILayout.TextField(thisScript.ActionTag);

                thisScript.ChangesSprite = GUILayout.Toggle(thisScript.ChangesSprite, "Change Sprite");
                if (thisScript.ChangesSprite) { thisScript.NewSprite = EditorGUILayout.ObjectField("New Sprite", thisScript.NewSprite, typeof(Sprite), false) as Sprite; }

                thisScript.MovesObject = GUILayout.Toggle(thisScript.MovesObject, "Move Object");
                if (thisScript.MovesObject) {
                    thisScript.ObjectToMove = EditorGUILayout.ObjectField("Object", thisScript.ObjectToMove, typeof(GameObject), true) as GameObject;
                    thisScript.MoveToWaypoint = EditorGUILayout.ObjectField("Waypoint", thisScript.MoveToWaypoint, typeof(GameObject), true) as GameObject;
                    if (thisScript.MoveToWaypoint != null) { thisScript.MoveToPosition = thisScript.MoveToWaypoint.transform.position; }
                    else { thisScript.MoveToPosition = thisScript.gameObject.transform.position;  }
                }

                thisScript.CreatesObject = GUILayout.Toggle(thisScript.CreatesObject, "Create Object");
                if (thisScript.CreatesObject) {
                    thisScript.ObjectToCreate = EditorGUILayout.ObjectField("Object Prefab", thisScript.ObjectToCreate, typeof(GameObject), true) as GameObject;
                    thisScript.CreateAtWaypoint = EditorGUILayout.ObjectField("Waypoint", thisScript.CreateAtWaypoint, typeof(GameObject), true) as GameObject;
                    if (thisScript.CreateAtWaypoint != null) { thisScript.CreateAtPosition = thisScript.CreateAtWaypoint.transform.position; }
                    else { thisScript.CreateAtPosition = thisScript.gameObject.transform.position; }
                }

                thisScript.PlaysSound = GUILayout.Toggle(thisScript.PlaysSound, "Play Sound");
                if (thisScript.PlaysSound) {
                    thisScript.Sound = EditorGUILayout.TextField(thisScript.Sound);
                    thisScript.SoundDelay = EditorGUILayout.FloatField(thisScript.SoundDelay);
                }
            }
        }
    }
}
#endregion
#endif