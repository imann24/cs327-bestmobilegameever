using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour {
    private Graphic fade;

	// Use this for initialization
	void Start () {
        fade = GetComponent<Image>();
        fade.CrossFadeAlpha(0f, 0f, true);
    }

    public void FadeOut(float time = 1f) { fade.CrossFadeAlpha(1f, time, true); }
    public void FadeIn(float time = 1f) { fade.CrossFadeAlpha(0f, time, true); }
}
