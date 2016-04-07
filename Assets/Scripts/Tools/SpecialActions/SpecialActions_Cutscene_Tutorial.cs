using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SpecialActions_Cutscene_Tutorial : SpecialActions {
	public Transform TutorialRoomDoor;

	public bool Testing = false;
    private GameObject Quartermaster, Shipmaster, Firstmate;
    private static SpecialActions_Cutscene_Tutorial _instance = null;
    private string next;

    void Start() {
        if (_instance != null) { Destroy(gameObject); }
        else {
            _instance = this;
            DontDestroyOnLoad(gameObject);
			Fader.FadeOut(2);
			if (SceneManager.GetActiveScene().name == "TutorialScene" || SceneManager.GetActiveScene().name == "TutorialSceneWithNavMesh") {
				EventController.Event("PlayerYawn");
                next = "tutorial_start";
                //EventController.Event("PromptAppears");
				NextInteraction(next, null, true, true);
            }
            else if (SceneManager.GetActiveScene().name == "WorldScene2") {
                if (Testing) {
                    Firstmate = GameObject.Find("Firstmate");
                    Firstmate.transform.position = Firstmate.transform.Find("Exit").position;
                    Destroy(GameObject.Find("Shipmaster"));
                    Destroy(GameObject.Find("Quartermaster"));
                }
                else {
                    setupTutorialCutscene();
                    NextInteraction(next);
                }
            }
            /*
            else if (SceneManager.GetActiveScene().name == "WorldScene2") {
                GameManager.GiveTag("firstmateIntro");
            }
            */
            else { Debug.LogWarning("Scene not recognized by Cutscene_Handler"); }
        }
    }

	// Override  in the tutorial scene to pass the position of the door
	public override Vector3 GetPosition () {
		if ((PSScene)SceneManager.GetActiveScene().buildIndex == PSScene.TutorialScene) {
			return TutorialRoomDoor.position;
		} else {
			return base.GetPosition();
		}
			
	}
    private void setupTutorialCutscene() {
        Quartermaster = GameObject.Find("Quartermaster");
        Shipmaster = GameObject.Find("Shipmaster");
        Firstmate = GameObject.Find("Firstmate");
        foreach (Transform child in GameObject.Find("WorldObjects").transform) {
            if (child.gameObject == GameObject.Find("MajorCharacters")) { child.gameObject.SetActive(true); }
            else { child.gameObject.SetActive(false); }
        }
        GameManager.InventoryManager.TakeItem("Hook");
        GameManager.InventoryManager.GiveItem("Hook");
        GameManager.InventoryManager.Hide();
        next = "tutorial_cutscene_start";
    }

    public override void DoSpecialAction(string actionTag) {
        switch (actionTag) {
            case "EndTutorialCutscene":
                StartCoroutine(EndTutorialCutscene());
                break;
			case "SoundTutorialPrompt":
				EventController.Event("PromptAppears");
			break;
            case "HideInventory":
                GameManager.InventoryManager.Hide();
                break;
            case "ExitTutorialRoom":
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

    private void destroyQM() { Destroy(Quartermaster); }
    private void destroySM() { Destroy(Shipmaster); }

    IEnumerator npcExit(GameObject npc) {
        GameManager.UIManager.LockScreen();
        Vector3 exit = npc.transform.Find("Exit").position;
        Move(npc, exit, 4, true);
        if (npc == Quartermaster) Invoke("destroyQM", 6f);
        else if (npc == Shipmaster) Invoke("destroySM", 6f);
        yield return new WaitForSeconds(3f);
        GameManager.UIManager.UnlockScreen();
        NextInteraction(next);
    }
    
    IEnumerator NextScene() {
        GameManager.UIManager.LockScreen();
		Fader.FadeIn ();
        GameManager.InventoryManager.Hide();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Scenes/WorldScene2");
        yield return new WaitForSeconds(1f);
        setupTutorialCutscene();
        Fader.FadeOut ();
        GameManager.UIManager.UnlockScreen();
        NextInteraction(next);
    }

    IEnumerator EndTutorialCutscene() {
        GameManager.UIManager.LockScreen();
		Fader.FadeIn ();
        GameManager.InventoryManager.Hide();
        yield return new WaitForSeconds(2f);
        foreach (Transform child in GameObject.Find("WorldObjects").transform) {
            child.gameObject.SetActive(true);
        }
        GameObject.Find("Test_Pete").SetActive(false);
        Fader.FadeOut();
        yield return new WaitForSeconds(1f);
        GameManager.UIManager.UnlockScreen();
    }
}
