#define DEBUG

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    private static UIManager _instance = null;
    public bool CanInteract = true;

    [SerializeField]
	GameObject tapToContinue = null;

	public GameObject screenFader;

    void Awake() {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (this != _instance) {
            Destroy(gameObject);
        }
    }

    public void EnableTapToContinue(Interactable interactor, Interaction interaction){
        CanInteract = false;
		tapToContinue.SetActive (true);
		tapToContinue.GetComponent<InteractionButton> ().interactor = interactor;
		tapToContinue.GetComponent<InteractionButton> ().interaction = interaction;
	}

	public void DisableTapToContinue(){
        CanInteract = true;
		tapToContinue.SetActive (false);
	}

	public void LockScreen() {
        CanInteract = false;
        tapToContinue.SetActive (true);
		tapToContinue.GetComponent<Button> ().enabled = false;
	}

	public void UnlockScreen() {
        CanInteract = true;
        tapToContinue.GetComponent<Button> ().enabled = true;
		tapToContinue.SetActive (false);
	}
}
