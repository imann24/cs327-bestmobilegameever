using UnityEngine;
using UnityEngine.UI;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ToggleDemoKeyword : MonoBehaviour {
	public bool ReplaceWithCustomText;

	[HideInInspector]
	public string CustomText;

	void Awake () {
		if (!GameTextUtil.ShowDemoKeyword) {
			Text myText = GetComponent<Text>();
			if (ReplaceWithCustomText) {
				myText.text = myText.text = CustomText;
			} else {
				myText.text = myText.text.Replace(GameTextUtil.DemoKeyword, string.Empty);
			}
		}
	}



}


#if UNITY_EDITOR
[CustomEditor(typeof(ToggleDemoKeyword))]
public class ToggleDemoKeywordEditor : Editor {
	public override void OnInspectorGUI() {
		var thisScript = target as ToggleDemoKeyword;
		if (thisScript) {
			thisScript.ReplaceWithCustomText = GUILayout.Toggle(thisScript.ReplaceWithCustomText, "Use Custom Text");
			if (thisScript.ReplaceWithCustomText) {
				thisScript.CustomText = EditorGUILayout.TextField(thisScript.CustomText);
			}
		}
	}
}
#endif