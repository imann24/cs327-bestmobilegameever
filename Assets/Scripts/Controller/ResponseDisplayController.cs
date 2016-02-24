/*
 * Author: Isaiah Mann
 * Description: Controls the display of dialogue resposnes
 */

using UnityEngine;
using System.Collections;

public class ResponseDisplayController : MonoBehaviour {

	VisualResponse[] _responsePanels;

	bool _panelsNotYetInitialized {
		get {
			return _responsePanels == null;
		}
	}

	void Awake () {
		init();
	}

	public void Hide () {
		checkResponsePanels();

		for (int i = 0; i < _responsePanels.Length; i++) {
			_responsePanels[i].Hide();
		}
	}

	public void Show (params string [] responses) {
		checkResponsePanels();

		for (int i = 0; i < responses.Length; i++) {
			_responsePanels[i].Show(responses[i]);
		}

		for (int i = responses.Length; i < _responsePanels.Length; i++) {
			_responsePanels[i].Hide();
		}
	}

	void init () {
		setResponsePanels();
	}


	void setResponsePanels () {

		_responsePanels = new VisualResponse[transform.childCount];
			
		for (int i = 0; i < transform.childCount; i++) {
			_responsePanels[i] = transform.GetChild(i).GetComponent<VisualResponse>();
		}

	}

	void checkResponsePanels () {
		if (_panelsNotYetInitialized) {
			setResponsePanels();
		}
	}
}