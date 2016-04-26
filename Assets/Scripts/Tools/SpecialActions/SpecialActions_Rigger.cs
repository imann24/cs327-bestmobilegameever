using UnityEngine;
using System.Collections;

public class SpecialActions_Rigger : SpecialActions {
    private string next;
	string inRiggingKey = "inRigging";

    public override void DoSpecialAction(string actionTag) {
		switch (actionTag) {
            case "RiggerDescend":
                StartCoroutine(RiggerDescend());
                break;
        }
    }

    IEnumerator RiggerDescend() {
		Collider collider = GetComponent<Collider>();

		Fader.FadeIn();

		float offset = 10f;

        yield return new WaitForSeconds(2f);
		collider.isTrigger = true;
		gameObject.transform.position = GameObject.Find("Waypoint_RiggerDescend").transform.position + Vector3.back * offset;
		collider.isTrigger = false;
        NextInteraction("light", GameObject.Find("Lantern").GetComponent<Interactable>());
        Fader.FadeOut();
        NextInteraction("rigger_descend");
		GetComponentInChildren<Animator>().SetBool(inRiggingKey, false);
    }
}
