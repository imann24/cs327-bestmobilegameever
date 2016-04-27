using UnityEngine;
using System.Collections;

public class SpecialActions_OJ : SpecialActions {

    public override void DoSpecialAction(string actionTag) {
		AudioController audio = GameObject.Find ("AudioController").GetComponent<AudioController> ();
        switch (actionTag) {
		case "OJSpeech":
			audio.VoiceEffect("OJSpeech");
			break;
        case "OJUp":
            gameObject.GetComponent<OrangeGuyController>().OrangeOut();
            break;
        case "OJDown":
            gameObject.GetComponent<OrangeGuyController>().OrangeIn();
            break;
        }
    }

    private void destroyMe() {
        Destroy(gameObject);
    }
}