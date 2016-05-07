using UnityEngine;
using System.Collections;

public class Translate : MonoBehaviour {
	public Vector3 Translation;
	public float WaitTime = 5f;
	void Start () {
		StartCoroutine(TimedTranslate());
	}

	IEnumerator TimedTranslate () {
		yield return new WaitForSeconds(WaitTime);
		transform.position += Translation;
	}
}
