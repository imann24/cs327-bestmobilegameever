#define DEBUG

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static UIManager _instance = null;
    public bool CanInteract = true;
	public bool paused = false;

    [SerializeField]
	GameObject tapToContinue = null;
	public AudioController audioController = null;

	public GameObject DimBackground;
	public GameObject HelpTextBox;
	public GameObject audioControllerWrap;

	public Image MuteButtonGraphic;
	public Sprite MuteButtonWhenVolumeOff;
	public Sprite MuteButtonWhenVolumeOn;

	public float MinTapDelay = 0.5f;

	List<InteractionButton> dialogueOptions = new List<InteractionButton>();

	void OnLevelWasLoaded (int level) {
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		if (PSSceneUtil.InGame(level)) {
			ShowInventoryPanel();
		} else {
			HideInventoryPanel();
			CleanupScene();
		}
	}

	void HideInventoryPanel () {
		GameManager.InventoryManager.ToggleActive(false);
	}

	void ShowInventoryPanel () {
		GameManager.InventoryManager.ToggleActive(true);
	}

    void Awake() {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);
			spawnTextBox();
			setMuteButtonBasedOnSettings();
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
		
	public void ToggleDialogueArrows (bool isActive) {
		foreach (InteractionButton dialoge in dialogueOptions) {
			dialoge.ToggleArrowActive(isActive);
		}
	}

	public void AddDialogueOption (InteractionButton dialogueOption) {
		dialogueOptions.Add(dialogueOption);
	}

	public void RemoveDialogueOption (InteractionButton dialogueOption) {
		dialogueOptions.Remove(dialogueOption);
	}

	public void Matey(){
		audioController.Matey ();
		//Do matey sound
	}

	public void ToggleMute(){
		audioController.ToggleFXMute ();
		audioController.ToggleMusicMute ();
		setMuteButtonBasedOnSettings();
		//Mute button
	}

	public void ClickSound() {
		audioController.ClickSound ();
	}

	public void Pause(){
		paused = !paused;
		audioController.ClickSound ();
	}

	public void Quit(){
		ScreenFader.FadeOut ();
		Invoke ("ReturnToMainMenu", 0.5f);
	}

	void ReturnToMainMenu(){
		CleanupScene();

		SceneController.LoadMainMenu ();

	}

	void CleanupScene () {
		GameObject gm = GameObject.Find ("GameManager");
		GameObject sadie = GameObject.Find ("Sadie");
		GameObject cs = GameObject.Find ("Cutscene_Handler");

		Destroy (gm);
		Destroy (sadie);
		Destroy (cs);

		EventController.Event (PSEventType.HideTextBox);

		Destroy (this.gameObject);

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

	IEnumerator TapDelay(){

		// Added for debugging purposes
		#if UNITY_EDITOR
			MinTapDelay = 0;
		#endif

		LockScreen ();
		yield return new WaitForSeconds (MinTapDelay);
		UnlockScreen();
	}

	void spawnTextBox() {

		GameObject textBox;

		// Spawns the text box turned off
		(textBox = (GameObject)Instantiate(HelpTextBox)).SetActive(false);
		DontDestroyOnLoad(textBox);
	}
		
	void setMuteButtonBasedOnSettings () {
		MuteButtonGraphic.sprite = (SettingsUtil.FXMuted && SettingsUtil.MusicMuted) ? 
			MuteButtonWhenVolumeOff :
			MuteButtonWhenVolumeOn;
	}
}
