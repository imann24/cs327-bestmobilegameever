using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SpecialActions_Cutscene_Tutorial : SpecialActions {
    private GameObject Quartermaster, Shipmaster, Firstmate;
    private static SpecialActions_Cutscene_Tutorial _instance = null;
    private string next;

    void Start() {
        if (_instance != null) { Destroy(gameObject); }
        else {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            //ScreenFader.FadeOut(0);
			Fader.FadeOut(2);
			if (SceneManager.GetActiveScene().name == "TutorialScene" || SceneManager.GetActiveScene().name == "TutorialSceneWithNavMesh") {
                EventController.Event("PlayerYawn");
                next = "tutorial_start";
                Invoke("doNext", 1f);
            }
            else if (SceneManager.GetActiveScene().name == "WorldScene") {
                Quartermaster = GameObject.Find("Quartermaster");
                Shipmaster = GameObject.Find("Shipmaster");
                Firstmate = GameObject.Find("Firstmate");
                GameManager.InventoryManager.GiveItem("Hook_Clean");
                GameManager.InventoryManager.Hide();
                NextInteraction("tutorial_cutscene_start");
            }
            else { Debug.LogWarning("Scene not recognized by Cutscene_Handler"); }
        }
    }

    public override void DoSpecialAction(string actionTag) {
        switch (actionTag) {
            case "TestSceneChange":
                StartCoroutine(TestSceneChange());
                break;
			case "SoundTutorialPrompt":
				EventController.Event("PromptAppears");
			break;
            case "HideInventory":
                GameManager.InventoryManager.Hide();
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

    private void doNext() {
        EventController.Event("PromptAppears");
        NextInteraction(next);
    }

    IEnumerator npcExit(GameObject npc) {
        GameManager.UIManager.LockScreen();
		Fader.FadeIn (1f);
		//ScreenFader.FadeOut(1f);
        yield return new WaitForSeconds(1f);
        Destroy(npc);
		Fader.FadeOut (1f);
        //ScreenFader.FadeIn(1f);
        yield return new WaitForSeconds(1f);
        GameManager.UIManager.UnlockScreen();
        NextInteraction(next);
    }
    
    IEnumerator NextScene() {
        GameManager.UIManager.LockScreen();
		Fader.FadeIn ();
        //ScreenFader.FadeOut();
        GameManager.InventoryManager.Hide();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Scenes/WorldScene");
		Fader.FadeOut ();
        //ScreenFader.FadeIn();
        yield return new WaitForSeconds(1f);
        Quartermaster = GameObject.Find("Quartermaster");
        Shipmaster = GameObject.Find("Shipmaster");
        Firstmate = GameObject.Find("Firstmate");
        GameManager.UIManager.UnlockScreen();
        NextInteraction(next);
    }

    IEnumerator TestSceneChange() {
        GameManager.UIManager.LockScreen();
		Fader.FadeIn ();
        //ScreenFader.FadeOut();
        GameManager.InventoryManager.Hide();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Scenes/Development/SiennaTestOld");
        //ScreenFader.FadeIn();
		Fader.FadeOut();
        yield return new WaitForSeconds(1f);
        GameManager.UIManager.UnlockScreen();
    }
}
