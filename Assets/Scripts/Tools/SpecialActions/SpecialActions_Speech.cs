using UnityEngine;
using System.Collections;

public class SpecialActions_Speech : SpecialActions {

    public override void DoSpecialAction(string actionTag) {
        switch (actionTag) {
		case "FirstMateSpeech":
			AudioController audio = GameObject.Find ("AudioController").GetComponent<AudioController> ();
			audio.VoiceEffect("FirstMateSpeech");
            break;
		case "S":
			break;
        }
    }

    private void destroyMe() {
        Destroy(gameObject);
    }
}