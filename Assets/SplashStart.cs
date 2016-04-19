using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SplashStart : MonoBehaviour {

	// Use this for initialization
	public Image background;// { get; private set; }
	public Image logo;
	public float lifeTime = 2f; // How long the logos are onscreen before fade
	public float fadeTime = 2f; // How long the fade takes

	const float UNITY_LOGO_TIME_IOS = 4.5f; // Unity Logo seems to be overlaid over the first scene on iOS (while it loads consecutively on Android)
	void Start () {

		#if UNITY_IOS
			lifeTime += UNITY_LOGO_TIME_IOS;
		#endif

		DontDestroyOnLoad(gameObject);
		StartCoroutine(TimedFadeOut(lifeTime));
		StartCoroutine(TimedToMainMenu(lifeTime + fadeTime));
	}

	void ToMainMenu(){
		SceneController.LoadMainMenu ();
		FadeTransition(true);
	}

	IEnumerator TimedToMainMenu (float waitTime) {
		yield return new WaitForSeconds(waitTime);

		ToMainMenu();
	}

	void FadeIn(){
//		background.CrossFadeAlpha (1f, fadeTime, false);
		logo.CrossFadeAlpha (1f, fadeTime, false);

	}

	void FadeTransition (bool destroyAfterFadeIn = false) {
		background.CrossFadeAlpha(0f, fadeTime, false);

		if (destroyAfterFadeIn) {
			StartCoroutine(TimedDestroy(fadeTime));
		}
	}

	IEnumerator TimedFadeOut (float waitTime) {
		yield return new WaitForSeconds(waitTime);
		FadeOut();
	}

	void FadeOut(){
//		background.CrossFadeAlpha (0f, fadeTime, false);
		logo.CrossFadeAlpha (0f, fadeTime, false);
	}


	IEnumerator TimedDestroy(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		Destroy(gameObject);
	}
}
