using UnityEngine;
using System.Collections;

public class AudioControllerWrap : MonoBehaviour {

	AudioController audioController;

	// Use this for initialization
	void Start () {
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
	}

	void OnLevelWasLoaded() {
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
	}
	
	public void Matey() {
		audioController.Matey ();
	}

	public void ClickSound() {
		audioController.ClickSound ();
	}

	public void SwabbieRun() {
		audioController.SwabbieRun ();
	}

	public void ToggleMute() {
		audioController.ToggleFXMute();
		audioController.ToggleMusicMute();
	}

	void OnDestroy(){
		this.audioController = null;
	}
}
