using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class TagUpdate : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<Text> ().text = TagManager.Instance.PlayerTags.Count + " Tags: " + string.Join (" ", TagManager.Instance.PlayerTags.ToArray ());
	}
}
