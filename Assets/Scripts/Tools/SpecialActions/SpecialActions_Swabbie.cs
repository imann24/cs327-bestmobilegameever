using UnityEngine;
using System.Collections;

public class SpecialActions_Swabbie : SpecialActions {

    public override void DoSpecialAction(string actionTag) {
        switch (actionTag) {
		case "SwabbieFlee":
			Invoke ("destroyMe", 3f);
            break;
		case "StopMopping":
			AudioController audio = GameObject.Find ("AudioController").GetComponent<AudioController> ();
			audio.SwabbieRun ();
			break;
        }
    }

    private void destroyMe() {
        Destroy(gameObject);
    }
}