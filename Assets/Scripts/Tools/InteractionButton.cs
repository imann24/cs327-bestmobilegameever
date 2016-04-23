using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;

public class InteractionButton : MonoBehaviour{
	public Interactable interactor;
	public Interaction interaction;
	public ButtonType Type = ButtonType.Generic;

	void OnDestroy () {
		if (Type == ButtonType.DialogeOption) {
			removeUIManagerFromList();
		}
	}

	const string ARROW_KEYWORD = "Arrow";
	GameObject _arrow;

	public void CompleteInteraction(){
		GameManager.InteractionManager.ClearInteractions ();
		InteractionManager.CompleteInteraction (interactor, interaction);
	}

	public void ToggleArrowActive (bool active) {
		_arrow.SetActive(active);	
	}

	public void SetAsDialogueOption () {
		setReferences();
		Type = ButtonType.DialogeOption;
	}

	void setReferences () {
		setArrow();
		addToUIManagerList();
	}

	void addToUIManagerList () {
		UIManager._instance.AddDialogueOption(this);
	}

	void removeUIManagerFromList () {
		UIManager._instance.RemoveDialogueOption(this);
	}

	void setArrow () {
		GameObject arrow;
		for (int i = 0; i < transform.childCount; i++) {
			if ((arrow = transform.GetChild(i).gameObject).name.Contains(ARROW_KEYWORD)) {
				this._arrow = arrow;
				_arrow.transform.localScale = new Vector3(1, 1, 1);
			}
		}
	}
}









