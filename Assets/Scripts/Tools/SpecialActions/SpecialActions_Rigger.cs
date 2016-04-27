using UnityEngine;
using System.Collections;

public class SpecialActions_Rigger : SpecialActions {
    public GameObject Waypoint = null;
    private string next;
	string inRiggingKey = "inRigging";
	AudioController audio;

    public override void DoSpecialAction(string actionTag) {
		switch (actionTag) {
            case "RiggerDescend":
                StartCoroutine(RiggerDescend());
                break;
			case "RiggerSpeech":
				audio = GameObject.Find ("AudioController").GetComponent<AudioController> ();
				audio.VoiceEffect ("RiggerSpeech");
				break;
        }
    }

    IEnumerator RiggerDescend() {
		Collider collider = GetComponent<Collider>();

		Fader.FadeIn();

		float offset = 10f;

        yield return new WaitForSeconds(2f);
		collider.isTrigger = true;
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
		gameObject.transform.position = Waypoint.transform.position; // + Vector3.back * offset;
		collider.isTrigger = false;
        NextInteraction("light", GameObject.Find("Lantern").GetComponent<Interactable>());
        Fader.FadeOut();
        NextInteraction("rigger_descend");
		GetComponentInChildren<Animator>().SetBool(inRiggingKey, false);
    }
}
