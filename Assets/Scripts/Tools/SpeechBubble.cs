using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpeechBubble : MonoBehaviour {
    private Interactable interactor;
    private Interaction interaction;

    public Color speechColor = Color.white;
	[SerializeField]
	private GameObject floatingText = null;

	public void Say(Interactable interactor, Interaction interaction) {
        this.interactor = interactor;
        this.interaction = interaction;
        GameManager.UIManager.LockScreen();
        Invoke("nextInteraction", 2f);

        GameObject go = Instantiate (floatingText);
		go.GetComponent<Text> ().text = interaction.iText;
		go.GetComponent<Text> ().color = speechColor;
		go.transform.SetParent (transform);
		go.transform.localScale = Vector3.one;
		go.GetComponent<RectTransform> ().localPosition = Vector3.zero;
	}

	void Update () {
		transform.LookAt (transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }

    private void nextInteraction() {
        GameManager.UIManager.UnlockScreen();
        InteractionManager.CompleteInteraction(interactor, interaction);
    }
}
