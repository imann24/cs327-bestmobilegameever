#define DEBUG

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    private static UIManager _instance = null;

    [SerializeField]
	GameObject tapToContinue = null;

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
		tapToContinue.SetActive (true);
		tapToContinue.GetComponent<InteractionButton> ().interactor = interactor;
		tapToContinue.GetComponent<InteractionButton> ().interaction = interaction;
	}

	public void DisableTapToContinue(){
		tapToContinue.SetActive (false);
	}

	public void LockScreen(){
		tapToContinue.SetActive (true);
		tapToContinue.GetComponent<Button> ().enabled = false;
	}

	public void UnlockScreen(){
		tapToContinue.GetComponent<Button> ().enabled = true;
		tapToContinue.SetActive (false);
	}
}
