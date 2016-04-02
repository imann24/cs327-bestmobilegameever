#define DEBUG

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static UIManager _instance = null;
    public bool CanInteract = true;
	public bool paused = false;
    [SerializeField]
	GameObject tapToContinue = null;

	public GameObject DimBackground;

    void Awake() {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (this != _instance) {
            Destroy(gameObject);
        }
    }

	void Update(){
		if (paused) {
			CanInteract = false;
		} else {
			CanInteract = true;
		}
	}

	public void Matey(){
		//Do matey sound
	}
	public void Pause(){
		paused = !paused;
	}

	public void Quit(){
		ScreenFader.FadeOut ();
		Invoke ("ReturnToMainMenu", 2f);
	}

	void ReturnToMainMenu(){
		SceneController.LoadMainMenu ();
	}

    public void EnableTapToContinue(Interactable interactor, Interaction interaction){
		
        CanInteract = false;
		tapToContinue.SetActive (true);
		DimBackground.SetActive (true);
		tapToContinue.GetComponent<InteractionButton> ().interactor = interactor;
		tapToContinue.GetComponent<InteractionButton> ().interaction = interaction;
	}

	public void DisableTapToContinue(){
		DimBackground.SetActive (false);
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
