using UnityEngine;
using System.Collections;

public class SpecialActions_Swabbie : SpecialActions {

    public override void DoSpecialAction(string actionTag) {
        switch (actionTag) {
            case "SwabbieFlee":
                Debug.Log("Invoking destroy...");
                Invoke("destroyMe", 3f);
                break;
        }
    }

    private void destroyMe() {
        Debug.Log("Attempting to destroy...");
        Destroy(gameObject);
    }
}