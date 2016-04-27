using UnityEngine;
using System.Collections;

public class SpecialActions_Swabbie : SpecialActions {

    public override void DoSpecialAction(string actionTag) {
		AudioController audio;
        switch (actionTag) {
		case "SwabbieFlee":
			Invoke ("destroyMe", 3f);
            break;
		case "StopMopping":
			audio = GameObject.Find ("AudioController").GetComponent<AudioController> ();
			audio.SwabbieRun ();
			break;
		case "SwabbieSpeech":
			audio = GameObject.Find ("AudioController").GetComponent<AudioController> ();
			audio.VoiceEffect ("SwabbieSpeech");
			break;
        }
    }

    private void destroyMe() {
        Destroy(gameObject);
    }
}