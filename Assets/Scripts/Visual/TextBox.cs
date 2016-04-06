/*
 * Author: Isaiah Mann
 * Description: Abstract representation of a text box
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class TextBox : TextElement {

	Text _text;

	public override void Show () {
		gameObject.SetActive(true);
	} 

	public override void Hide () {
		gameObject.SetActive(false);
	}

	public override void Set (params string[] text) {
		_text.text = text[0];
	}

	protected override void setReferences () {
		_text = GetComponentInChildren<Text>();
	}

}
