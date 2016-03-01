using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SpecialActions_Cutscene_Tutorial : SpecialActions {
    public GameObject Quartermaster;
    public GameObject Shipmaster;
    public GameObject Firstmate;
    private ScreenFader screenFader;

    public override void DoSpecialAction(string actionTag) {
        switch (actionTag) {
            case "ExitTutorialRoom":
                screenFader = GameObject.FindObjectOfType<ScreenFader>() as ScreenFader;
                DontDestroyOnLoad(screenFader);
                DontDestroyOnLoad(GameObject.Find("Cutscene_Handler"));
                StartCoroutine(NextScene());
                break;
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
    
    IEnumerator NextScene() {
        screenFader.FadeOut();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Scenes/WorldScene");
        screenFader.FadeIn();
        yield return new WaitForSeconds(1f);
        Debug.Log("Finished.");
    }
}
