using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InvPanel : MonoBehaviour {

	// Use this for initialization
	public bool isShowing {get; private set;}
	public Button toggle;


	public void Show(){
		gameObject.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, 50);
		isShowing = true;
		toggle.GetComponentInChildren<Text> ().text = "Hide";
	}

	public void Hide(){
		gameObject.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, -25);
		isShowing = false;
		toggle.GetComponentInChildren<Text> ().text = "Show";
	}

	public void Toggle(){
		if (isShowing) {
			Hide ();
		} else {
			Show ();
		}
	}

	void Start (){
		Show ();
	}

	//this mouse test will eventually go in the game manager, but we don't have one yet.
	void Update () {

	}
}