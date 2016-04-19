using UnityEngine;
using System.Collections;

public class SpecialActions_Speech : SpecialActions {

    public override void DoSpecialAction(string actionTag) {
		AudioController audio = GameObject.Find ("AudioController").GetComponent<AudioController> ();
        switch (actionTag) {
		case "FirstMateSpeech":
			audio.VoiceEffect("FirstMateSpeech");
            break;
		case "QuarterMasterSpeech":
			audio.VoiceEffect("SecondMateSpeech");
			break;
		case "RiggerSpeech":
			audio.VoiceEffect("RiggerSpeech");
			break;
		case "SwabbieSpeech":
			audio.VoiceEffect("SwabbieSpeech");
			break;
        }
    }

    private void destroyMe() {
        Destroy(gameObject);
    }
}