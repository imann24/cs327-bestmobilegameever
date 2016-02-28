using UnityEngine;
using System.Collections;

public class SpecialActions_Captain_Portrait : SpecialActions {
    public Sprite CaptainPortraitPainted;
    public Sprite CaptainPortraitDressed;

    public override void DoSpecialAction(string actionTag) {
        switch (actionTag) {
            case "ChangeToPainted":
                GetComponent<SpriteRenderer>().sprite = CaptainPortraitPainted;
                if (gameObject.GetComponent<Interactable>().Debugging) { Debug.Log("Attempting to paint."); }
                break;
            case "ChangeToDressed":
                GetComponent<SpriteRenderer>().sprite = CaptainPortraitDressed;
                if (gameObject.GetComponent<Interactable>().Debugging) { Debug.Log("Attempting to dress."); }
                break;
            default:
                if (gameObject.GetComponent<Interactable>().Debugging) {
                    Debug.Log(actionTag + " isn't defined in SpecialActions_Captain_Portrait.");
                }
                break;
        }
    }
}
