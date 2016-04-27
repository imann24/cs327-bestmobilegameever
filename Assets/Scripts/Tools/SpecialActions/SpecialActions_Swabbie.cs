using UnityEngine;
using System.Collections;

public class SpecialActions_Swabbie : SpecialActions {
	const string GIVE_UP_MOP_ANIMATOR_KEY = "GiveAwayMop";

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
		case GIVE_UP_MOP_ANIMATOR_KEY:
			GetComponent<Animator>().SetTrigger(actionTag);
			break;
        }
    }

    private void destroyMe() {
        Destroy(gameObject);
    }
}