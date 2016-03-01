using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SpecialActions_Cutscene_Tutorial : SpecialActions {
    private GameObject Quartermaster;
    private GameObject Shipmaster;
    private GameObject Firstmate;
    private string next;

    public override void DoSpecialAction(string actionTag) {
        switch (actionTag) {
            case "ExitTutorialRoom":
                next = "tutorial_cutscene_start";
                StartCoroutine(NextScene());
                break;
            case "QuartermasterExit":
                next = "tutorial_cutscene_exit_QM";
                StartCoroutine(npcExit(Quartermaster));
                if (gameObject.GetComponent<Interactable>().Debugging) { Debug.Log("Exit Quartermaster"); }
                break;
            case "ShipmasterExit":
                next = "tutorial_cutscene_exit_SM";
                StartCoroutine(npcExit(Shipmaster));
                if (gameObject.GetComponent<Interactable>().Debugging) { Debug.Log("Exit Shipmaster"); }
                break;
            case "FirstmateExit":
                next = "tutorial_cutscene_exit_FM";
                StartCoroutine(npcExit(Firstmate));
                if (gameObject.GetComponent<Interactable>().Debugging) { Debug.Log("Exit Firstmate"); }
                break;
            default:
                if (gameObject.GetComponent<Interactable>().Debugging) { Debug.Log(actionTag + " isn't defined in SpecialActions_Cutscene_Handler."); }
                break;
        }
    }

    IEnumerator npcExit(GameObject npc) {
        ScreenFader.FadeOut(1f);
        yield return new WaitForSeconds(1f);
        Destroy(npc);
        ScreenFader.FadeIn(1f);
        yield return new WaitForSeconds(1f);
        nextInteraction();
    }
    
    IEnumerator NextScene() {
        DontDestroyOnLoad(gameObject);
        ScreenFader.FadeOut();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Scenes/Development/SiennaTest2");
        ScreenFader.FadeIn();
        yield return new WaitForSeconds(1f);
        Quartermaster = GameObject.Find("Quartermaster");
        Shipmaster = GameObject.Find("Shipmaster");
        Firstmate = GameObject.Find("Firstmate");
        nextInteraction();
    }

    private void nextInteraction() {
        Interactable interactor = gameObject.GetComponent<Interactable>();
        List<Interaction> iList = interactor.Interactions.FindAll(i => (i.iName == next) && (i.iType == InteractionType.Derivative));
        if (gameObject.GetComponent<Interactable>().Debugging) { Debug.Log("Attempting to run interaction, interactor: " + interactor + "list: " + iList); }
        InteractionManager.HandleInteractionList(interactor, iList);
    }
}
