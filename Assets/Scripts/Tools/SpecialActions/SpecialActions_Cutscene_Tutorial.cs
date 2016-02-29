using UnityEngine;
using System.Collections;

public class SpecialActions_Cutscene_Tutorial : SpecialActions {
    public GameObject Quartermaster;
    public GameObject Shipmaster;
    public GameObject Firstmate;

    public override void DoSpecialAction(string actionTag) {
        switch (actionTag) {
            case "QuartermasterExit":
                npcExit(Quartermaster);
                if (gameObject.GetComponent<Interactable>().Debugging) { Debug.Log("Exit Quartermaster"); }
                break;
            case "ShipmasterExit":
                npcExit(Shipmaster);
                if (gameObject.GetComponent<Interactable>().Debugging) { Debug.Log("Exit Shipmaster"); }
                break;
            case "FirstmateExit":
                npcExit(Firstmate);
                if (gameObject.GetComponent<Interactable>().Debugging) { Debug.Log("Exit Firstmate"); }
                break;
            default:
                if (gameObject.GetComponent<Interactable>().Debugging) { Debug.Log(actionTag + " isn't defined in SpecialActions_Cutscene_Handler."); }
                break;
        }
    }

    private void npcExit(GameObject npc) {
        Destroy(npc);
    }
}
