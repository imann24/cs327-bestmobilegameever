using UnityEngine;
using System.Collections;

public class SpecialActions_Cutscene_Tutorial : SpecialActions {
    public GameObject Quartermaster;
    public GameObject Shipmaster;
    public GameObject Firstmate;
    public Vector2 QuartermasterExit;
    public Vector2 ShipmasterExit;
    public Vector2 FirstmateExit;

    public override void DoSpecialAction(string actionTag) {
        switch (actionTag) {
            case "QuartermasterExit":
                npcExit(Quartermaster, QuartermasterExit);
                if (gameObject.GetComponent<Interactable>().Debugging) { Debug.Log("Exit Quartermaster"); }
                break;
            case "ShipmasterExit":
                npcExit(Shipmaster, ShipmasterExit);
                if (gameObject.GetComponent<Interactable>().Debugging) { Debug.Log("Exit Shipmaster"); }
                break;
            case "FirstmateExit":
                npcExit(Firstmate, FirstmateExit);
                if (gameObject.GetComponent<Interactable>().Debugging) { Debug.Log("Exit Firstmate"); }
                break;
            default:
                if (gameObject.GetComponent<Interactable>().Debugging) { Debug.Log(actionTag + " isn't defined in SpecialActions_Cutscene_Handler."); }
                break;
        }
    }

    private void npcExit(GameObject npc, Vector2 exit) {
        Destroy(npc);
    }
}
