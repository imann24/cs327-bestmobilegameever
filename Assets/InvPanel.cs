using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InvPanel : MonoBehaviour {

	// Use this for initialization
	public bool isShowing {get; private set;}
	public Button toggle;


	public void Show(){
		gameObject.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, 50);
	}

	public void Hide(){
		gameObject.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, -25);
	}

	public void Toggle(){
		if (isShowing) {
			Hide ();
			isShowing = false;
			toggle.GetComponentInChildren<Text> ().text = "Show";
		} else {
			Show ();
			isShowing = true;
			toggle.GetComponentInChildren<Text> ().text = "Hide";
		}
	}

	void Start (){
		isShowing = true;
	}

	//this mouse test will eventually go in the game manager, but we don't have one yet.
	void Update () {
		
	}
}
