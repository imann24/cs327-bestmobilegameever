using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SplashStart : MonoBehaviour {

	// Use this for initialization
	public Image background;// { get; private set; }
	public Image logo;
	public float fadeTime = 2f;

	void Start () {
		FadeOut ();
		Invoke ("ToMainMenu", 2f);
	}

	void ToMainMenu(){
		SceneController.LoadMainMenu ();
	}
	void FadeIn(){
//		background.CrossFadeAlpha (1f, fadeTime, false);
		logo.CrossFadeAlpha (1f, fadeTime, false);
	}

	void FadeOut(){
//		background.CrossFadeAlpha (0f, fadeTime, false);
		logo.CrossFadeAlpha (0f, fadeTime, false);
	}
		
}
