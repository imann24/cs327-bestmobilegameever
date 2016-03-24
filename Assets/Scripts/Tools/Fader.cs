using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fader : MonoBehaviour {

	static Image Screen;// { get; private set; }
	static float fadeTime;

	void Awake(){
		if (Screen == null && GetComponent<Image>() != null) {
			Screen = gameObject.GetComponent<Image>();
		} else {
			Destroy (this);
		}
	}
		
	public static void FadeIn( float time = 2f){
		Screen.CrossFadeAlpha (1f, time, false);
		Screen.GetComponent<Fader>().Invoke ("Enable", time);
	}

	public static void FadeOut( float time = 2f){
		Screen.CrossFadeAlpha (0f, time, false);
		Screen.GetComponent<Fader>().Invoke("Disable", time);
	}

	void Disable(){
		Screen.raycastTarget = false;
	}

	void Enable(){
		Screen.raycastTarget = true;
	}
}
