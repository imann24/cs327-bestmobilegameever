/*
 * Author: Isaiah Mann
 * Description: Stores a single credit
 */

using UnityEngine;
using System.Collections;

public class Credit {
	public string Name;

	public Credit (string name) {
		this.Name = name;
	}

	public override string ToString () {
		return string.Format ("[Credit] {0}", Name);
	}

}
