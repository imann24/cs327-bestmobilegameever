/*
 * Author: Isaiah Mann
 * Description: Astract representation of a text element
 */

using UnityEngine;
using System.Collections;

public abstract class TextElement : MonoBehaviour {

	void Awake () {
		subscribeEvents();
		setReferences();
	}

	void OnDestroy () {
		unsubscribeEvents();
	}

	// Visual methods
	public abstract void Show();

	public abstract void Hide();

	public abstract void Set(params string[] text);

	// Events related classes	
	protected abstract void subscribeEvents ();

	protected abstract void unsubscribeEvents ();

	protected abstract void setReferences();
}
