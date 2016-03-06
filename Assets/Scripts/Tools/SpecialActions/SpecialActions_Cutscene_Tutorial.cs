using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SpecialActions_Cutscene_Tutorial : SpecialActions {
    public AudioSource Yawn;
    private GameObject Quartermaster, Shipmaster, Firstmate;
    private string next;

    void Start() {
        ScreenFader.FadeOut(0);
        Yawn.Play();
        next = "tutorial_start";
        Invoke("doNext", 1f);
    }

    public override void DoSpecialAction(string actionTag) {
        switch (actionTag) {
			case "PlayPromptSound":
				EventController.Event("PromptAppears");
			break;
			case "PlayOrangeSplatSound":
				EventController.Event("OrangeImpact");
			break;
			case "PlayRagDropSound":
				EventController.Event("RagDrop");
			break;
			case "PlayRubbingSound":
				EventController.Event("RagOnHand");
			break;
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

    private void doNext() { NextInteraction(next); }

    IEnumerator npcExit(GameObject npc) {
        GameManager.UIManager.LockScreen();
        ScreenFader.FadeOut(1f);
        yield return new WaitForSeconds(1f);
        Destroy(npc);
        ScreenFader.FadeIn(1f);
        yield return new WaitForSeconds(1f);
        GameManager.UIManager.UnlockScreen();
        NextInteraction(next);
    }
    
    IEnumerator NextScene() {
        GameManager.UIManager.LockScreen();
        DontDestroyOnLoad(gameObject);
        ScreenFader.FadeOut();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Scenes/Development/SiennaTest2");
        ScreenFader.FadeIn();
        yield return new WaitForSeconds(1f);
        Quartermaster = GameObject.Find("Quartermaster");
        Shipmaster = GameObject.Find("Shipmaster");
        Firstmate = GameObject.Find("Firstmate");
        GameManager.UIManager.UnlockScreen();
        NextInteraction(next);
    }
}
