using UnityEngine;
using System.Collections;

public class SpecialActions_Rigger : SpecialActions {
    private string next;

    public override void DoSpecialAction(string actionTag) {
        switch (actionTag) {
            case "RiggerDescend":
                StartCoroutine(RiggerDescend());
                break;
        }
    }

    IEnumerator RiggerDescend() {
        Fader.FadeIn();
        yield return new WaitForSeconds(2f);
        gameObject.transform.position = GameObject.Find("Waypoint_RiggerDescend").transform.position;
        NextInteraction("light", GameObject.Find("Lantern").GetComponent<Interactable>());
        Fader.FadeOut();
        NextInteraction("rigger_descend");
    }
}
