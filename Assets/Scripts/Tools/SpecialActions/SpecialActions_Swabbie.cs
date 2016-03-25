using UnityEngine;
using System.Collections;

public class SpecialActions_Swabbie : SpecialActions {

    public override void DoSpecialAction(string actionTag) {
        switch (actionTag) {
            case "SwabbieFlee":
                Invoke("destroyMe", 3f);
                break;
        }
    }

    private void destroyMe() {
        Destroy(gameObject);
    }
}