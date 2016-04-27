/*
 * Author: Isaiah Mann
 * Description: Triggers animation changes in the Firstmate
 */

using UnityEngine;
using System.Collections;

public class FirstmateController : MonoBehaviour {
	const string WEARING_MOP_KEY = "Mop";
	Animator animator;

	void Awake () {
		setReferences();
		subscribeEvents();
	}

	void OnDestroy () {
		unsubscribeEvents();
	}

	public void PutOnMopWig () {
		animator.SetTrigger(WEARING_MOP_KEY);
	}
		
	void setReferences () {
		animator = GetComponent<Animator>();
	}

	void subscribeEvents () {
		EventController.OnNamedEvent += handleNamedEvent;
	}

	void unsubscribeEvents () {
		EventController.OnNamedEvent -= handleNamedEvent;
	}

	void handleNamedEvent (string eventName) {
		if (eventName == EventList.FIRST_MATE_PUT_ON_WIG) {
			PutOnMopWig();
		}
	}


}
