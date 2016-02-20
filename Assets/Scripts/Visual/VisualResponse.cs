/*
 * Author: Isaiah Mann
 * Description: Controls a single response text
 */
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VisualResponse : MonoBehaviour {

	const string RESPONSE_TEXT_KEY = "ResponseText";

	bool _responseTextNotYetInitiailized {
		get {
			return _responseText == null;
		}
	}

	Text _responseText;

	void Awake () {
		init();
	}

	public void Show (string response) {
		checkResponseText();
		gameObject.SetActive(true);
		_responseText.text = response;

	}

	public void Hide () {
		checkResponseText();
		gameObject.SetActive(false);
	}

	void init () {
		setResponseText();
	}

	void setResponseText () {
		for (int i = 0; i < transform.childCount; i++) {
			if (isResponseObject(transform.GetChild(i).gameObject)) {
				_responseText = transform.GetChild(i).GetComponent<Text>();
			}
		}
	}

	bool isResponseObject (GameObject objectToTest) {
		return objectToTest.name == RESPONSE_TEXT_KEY;
	}

	void checkResponseText () {
		if (_responseTextNotYetInitiailized) {
			setResponseText();
		}
	}
}