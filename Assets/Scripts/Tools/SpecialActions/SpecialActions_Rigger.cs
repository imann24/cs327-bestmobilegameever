using UnityEngine;
using System.Collections;

public class SpecialActions_Rigger : SpecialActions {
    private string next;

    public override void DoSpecialAction(string actionTag) {
        switch (actionTag) {
            case "RiggerDescend":
                next = "rigger_descend";
                Invoke("doNext", 1f);
                break;
        }
    }

    private void doNext() {
        NextInteraction(next);
    }
}
