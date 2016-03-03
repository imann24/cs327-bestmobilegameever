using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpeechBubble : MonoBehaviour {

	public Color speechColor = Color.white;
	[SerializeField]
	private GameObject floatingText = null;

	public void Say(string textToSay){
		GameObject go = Instantiate (floatingText);
		go.GetComponent<Text> ().text = textToSay;
		go.GetComponent<Text> ().color = speechColor;
		go.transform.SetParent (transform);
		go.transform.localScale = Vector3.one;
		go.GetComponent<RectTransform> ().localPosition = Vector3.zero;
	}

	void Update () {
		transform.LookAt (transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
	}
}
