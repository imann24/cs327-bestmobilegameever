using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpeechBubble : MonoBehaviour {
    private Interactable interactor;
    private Interaction interaction;

	private WorldController controller;


    public Color speechColor = Color.white;
	[SerializeField]
	private GameObject floatingText = null;

	void Start(){
		GetComponent<Canvas> ().worldCamera = Camera.main;
	}

	public void Say(Interactable interactor, Interaction interaction) {
        this.interactor = interactor;
        this.interaction = interaction;
        GameManager.UIManager.LockScreen();

		GameObject go = Instantiate (floatingText,new Vector3(0,-12,0), Quaternion.Euler(new Vector3(90,0,0))) as GameObject;
		go.GetComponent<Text> ().text = interaction.iText;

		go.GetComponent<Text> ().color = speechColor;
		go.transform.SetParent (transform);
		go.transform.localScale = Vector3.one;
		go.GetComponent<RectTransform> ().localPosition = Vector3.zero;
	}

	void Update () {
		transform.LookAt (transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }

    public void NextInteraction() {
        GameManager.UIManager.UnlockScreen();
        InteractionManager.CompleteInteraction(interactor, interaction);
    }
}
