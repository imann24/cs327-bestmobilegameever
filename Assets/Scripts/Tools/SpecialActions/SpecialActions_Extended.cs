using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class SpecialActions_Extended : MonoBehaviour
{
    public string ActionTag;
    public bool ChangesSprite;
    public Sprite NewSprite;
    public bool MovesObject;
    public GameObject ObjectToMove;
    public Vector2 TargetPosition;
    public bool DestroysObject;
    public GameObject ObjectToDestroy;

    public void DoExtendedAction() {
        if(ChangesSprite) { GetComponent<SpriteRenderer>().sprite = NewSprite; }
        if(MovesObject) { ObjectToMove.transform.position = new Vector3(TargetPosition.x, TargetPosition.y, ObjectToMove.transform.position.z); }
        if(DestroysObject) { DestroyObject(ObjectToDestroy); }
    }
}

[CustomEditor(typeof(SpecialActions_Extended))]
public class SpecialActionsEditor : Editor {
    public override void OnInspectorGUI() {
        var thisScript = target as SpecialActions_Extended;
        thisScript.ActionTag = EditorGUILayout.TextField(thisScript.ActionTag);

        thisScript.ChangesSprite = GUILayout.Toggle(thisScript.ChangesSprite, "Changes Sprite");
        if (thisScript.ChangesSprite) { thisScript.NewSprite = EditorGUILayout.ObjectField("New Sprite", thisScript.NewSprite, typeof(Sprite), false) as Sprite; }

        thisScript.MovesObject = GUILayout.Toggle(thisScript.MovesObject, "Moves Object");
        if (thisScript.MovesObject) {
            thisScript.ObjectToMove = EditorGUILayout.ObjectField("Object", thisScript.ObjectToMove, typeof(GameObject), true) as GameObject;
            thisScript.TargetPosition = EditorGUILayout.Vector2Field("Target Position", thisScript.TargetPosition);
        }

        //thisScript.DestroysObject = GUILayout.Toggle(thisScript.DestroysObject, "Destroys Object");
        if (thisScript.DestroysObject) { thisScript.ObjectToDestroy = EditorGUILayout.ObjectField("Object", thisScript.ObjectToDestroy, typeof(GameObject), true) as GameObject; }
    }
}
