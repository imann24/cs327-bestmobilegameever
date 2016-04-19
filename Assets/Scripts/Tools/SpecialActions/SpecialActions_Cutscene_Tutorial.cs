using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SpecialActions_Cutscene_Tutorial : SpecialActions {
	//public Transform TutorialRoomDoor;

	public bool Testing = false;
    private GameObject Quartermaster, Shipmaster, Firstmate;
    private static SpecialActions_Cutscene_Tutorial _instance = null;
    private string next;

    public GameObject BGClouds = null;
    public GameObject BGBlack = null;
    private GameObject portraitMoving = null;
    private Vector3 portraitPosition;
    private Vector3 portraitDestination;

    void Start() {
        if (_instance != null) { Destroy(gameObject); }
        else {
            _instance = this;
            DontDestroyOnLoad(gameObject);
			Fader.FadeOut(2);
			if (SceneManager.GetActiveScene().name == "TutorialScene" || SceneManager.GetActiveScene().name == "TutorialSceneWithNavMesh") {
                next = "tutorial_start";
                if (BGBlack != null) BGBlack.SetActive(true);
				NextInteraction(next, null, true, true);
            }
            else if (SceneManager.GetActiveScene().name == "WorldScene2") {
                setupTutorialCutscene();
                if (!Testing) NextInteraction(next);
            }
            else { Debug.LogWarning("Scene not recognized by Cutscene_Handler"); }
        }
    }

    void Update() {
        if (portraitMoving != null) {
            if ((portraitMoving.transform.position - portraitDestination).magnitude < 0.1f) {
                GameManager.UIManager.UnlockScreen();
                GameManager.InteractionManager.ClearInteractions();
                NextInteraction(next);
                portraitMoving.transform.position = portraitPosition;
                portraitMoving = null;
            }
            else {
                //Debug.Log("Moving portrait: " + portraitMoving + " to " + portraitDestination);
                portraitMoving.transform.position = Vector3.MoveTowards(portraitMoving.transform.position, portraitDestination, (150 * Time.deltaTime));
            }
        }
    }

    private void setupTutorialCutscene() {
        /*
        foreach (Transform child in GameObject.Find("WorldObjects").transform) {
            if (child.gameObject == GameObject.Find("MajorCharacters")) { child.gameObject.SetActive(true); }
            else { child.gameObject.SetActive(false); }
        }
        
        Quartermaster = GameObject.Find("Quartermaster");
        Shipmaster = GameObject.Find("Shipmaster");
        Firstmate = GameObject.Find("Firstmate");
        Firstmate.transform.position = GameObject.Find("Waypoint_Firstmate").transform.position;
        Destroy(Shipmaster);
        Destroy(Quartermaster);
        */

        if (!Testing) { GameObject.Find("Test_Pete").SetActive(false); }
        GameManager.InventoryManager.TakeItem("Hook");
        GameManager.InventoryManager.GiveItem("Hook");
        GameManager.InventoryManager.Hide();
        next = "tutorial_cutscene_start";

        if (BGClouds != null) BGClouds.SetActive(true);
        else if (BGBlack != null) BGBlack.SetActive(true);
    }

    public override void DoSpecialAction(string actionTag) {
        switch (actionTag) {
            case "ClearBG":
                if (BGBlack != null) BGBlack.SetActive(false);
                break;
            case "EndTutorialCutscene":
                //foreach (Graphic g in BG.GetComponentsInChildren<Graphic>()) { g.CrossFadeAlpha(0f, 2f, true); }
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
                portraitExit(-1);
                //StartCoroutine(npcExit(Quartermaster));
                if (gameObject.GetComponent<Interactable>().Debugging) { Debug.Log("Exit Quartermaster"); }
                break;
            case "ShipmasterExit":
                next = "tutorial_cutscene_exit_SM";
                portraitExit(1);
                //StartCoroutine(npcExit(Shipmaster));
                if (gameObject.GetComponent<Interactable>().Debugging) { Debug.Log("Exit Shipmaster"); }
                break;
            case "FirstmateExit":
                next = "tutorial_cutscene_exit_FM";
                portraitExit(-1);
                //StartCoroutine(npcExit(Firstmate));
                if (gameObject.GetComponent<Interactable>().Debugging) { Debug.Log("Exit Firstmate"); }
                break;
            default:
                if (gameObject.GetComponent<Interactable>().Debugging) { Debug.Log(actionTag + " isn't defined in SpecialActions_Cutscene_Handler."); }
                break;
        }
    }

    private void portraitExit(float dir) {
        GameManager.UIManager.LockScreen();

        if (dir > 0) { portraitMoving = GameManager.InteractionManager.rightImage; }
        else { portraitMoving = GameManager.InteractionManager.leftImage; }

        portraitPosition = portraitMoving.transform.position;
        float dest = (portraitPosition.x + (dir * 300));
        portraitDestination = new Vector3(dest, portraitPosition.y, portraitPosition.z);
    }

    IEnumerator EndTutorialCutscene() {
        GameManager.UIManager.DimBackground.SetActive(true);
        GameManager.UIManager.LockScreen();
        Fader.FadeIn(1f);
        yield return new WaitForSeconds(1f);
        GameManager.UIManager.DimBackground.SetActive(false);
        if (BGClouds != null) BGClouds.SetActive(false);
        else if (BGBlack != null) BGBlack.SetActive(false);
        Fader.FadeOut(2f);
        GameManager.UIManager.UnlockScreen();
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
}
