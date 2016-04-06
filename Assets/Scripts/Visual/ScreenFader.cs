using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour {
    static Graphic fade = null;

	// Use this for initialization
	void Awake () {
        if (fade != null) { Destroy(gameObject); }
        else {
            fade = GetComponent<Image>();
            FadeIn(0f);
            DontDestroyOnLoad(gameObject);
        }
    }

    public static void FadeOut(float time = 2f) { 
		if (fade != null) {
			fade.CrossFadeAlpha(1f, time, true); 
		}
	}
    
	public static void FadeIn(float time = 2f) { 
		if (fade != null) {
			fade.CrossFadeAlpha(0f, time, true); 
		}
	}
}
