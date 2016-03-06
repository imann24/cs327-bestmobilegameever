using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class SpecialActions : MonoBehaviour {
    private Dictionary<string, SpecialActions> actionScripts = new Dictionary<string, SpecialActions>();

    // Possible Extended Special Actions & Parameters
    // Always HideInInspector so they don't show in scripts that inherit them
    // The SpecialActionsEditor handles what is shown
    [HideInInspector]
    public string ActionTag = null;
    [HideInInspector]
    public bool UseExtended, ChangesSprite, MovesObject, CreatesObject, PlaysSound;
    [HideInInspector]
    public Sprite NewSprite;
    [HideInInspector]
    public GameObject ObjectToMove, ObjectToCreate;
    [HideInInspector]
    public Vector2 MoveToPosition, CreateAtPosition;
    [HideInInspector]
    public AudioSource Sound;
    [HideInInspector]
    public float SoundDelay = 0;

    public void Awake() {
        SpecialActions[] scripts = GetComponents<SpecialActions>();
        foreach (SpecialActions script in scripts) {
            if (script.ActionTag != null) { actionScripts.Add(script.ActionTag, script); }
        }
    }

    public void DoSpecialActions(List<string> actionList){
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

    public void DoExtendedAction() {
        if (ChangesSprite) { GetComponent<SpriteRenderer>().sprite = NewSprite; }
        if (MovesObject) { ObjectToMove.transform.position = new Vector3(MoveToPosition.x, MoveToPosition.y, ObjectToMove.transform.position.z); }
        if (CreatesObject) {
            GameObject newObject = (GameObject)Instantiate(ObjectToCreate, new Vector3(CreateAtPosition.x, CreateAtPosition.y, gameObject.transform.position.z), Quaternion.identity);
            newObject.name = ObjectToCreate.name;
        }
        if (PlaysSound) {
            if (SoundDelay == 0) { Sound.Play(); }
            else { Sound.PlayDelayed(SoundDelay); }
        }
    }

    public void NextInteraction(string name, Interactable interactor = null) {
        if (interactor == null) { interactor = gameObject.GetComponent<Interactable>(); }
        List<Interaction> iList = interactor.Interactions.FindAll(i => (i.iName == name) && (i.iType == InteractionType.Derivative));
        if (gameObject.GetComponent<Interactable>().Debugging) { Debug.Log("Attempting to run interaction with name '" + name + "' that belongs to " + interactor); }
        InteractionManager.HandleInteractionList(interactor, iList);
    }

    //override this function in subclasses for specific actions.
    public virtual void DoSpecialAction(string actionTag) {
		if (gameObject.GetComponent<Interactable> ().Debugging) {
			Debug.Log (actionTag + " is not defined. Using default SpecialActions script.");
		}
	}
}

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
                    thisScript.MoveToPosition = EditorGUILayout.Vector2Field("Target Position", thisScript.MoveToPosition);
                }

                thisScript.CreatesObject = GUILayout.Toggle(thisScript.CreatesObject, "Create Object");
                if (thisScript.CreatesObject) {
                    thisScript.ObjectToCreate = EditorGUILayout.ObjectField("Object Prefab", thisScript.ObjectToCreate, typeof(GameObject), true) as GameObject;
                    thisScript.CreateAtPosition = EditorGUILayout.Vector2Field("Position", thisScript.CreateAtPosition);
                }

                thisScript.PlaysSound = GUILayout.Toggle(thisScript.PlaysSound, "Play Sound");
                if (thisScript.PlaysSound) {
                    thisScript.Sound = EditorGUILayout.ObjectField("Sound", thisScript.Sound, typeof(AudioSource), true) as AudioSource;
                    thisScript.SoundDelay = EditorGUILayout.FloatField(thisScript.SoundDelay);
                }
            }
        }
    }
}