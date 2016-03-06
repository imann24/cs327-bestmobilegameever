using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SpecialActions_Door : SpecialActions {
	private GameObject Sadie;
	private string next;

	public override void DoSpecialAction(string actionTag) {
		switch (actionTag) {
		case "EnterTutorial":
			next = "tutorial_cutscene_start";
			//StartCoroutine(NextScene());
			break;
		case "SoundOpenDoor":
			next = "tutorial_cutscene_exit_QM";
			if (gameObject.GetComponent<Interactable>().Debugging) { Debug.Log("Exit"); }
			break;
		default:
			if (gameObject.GetComponent<Interactable>().Debugging) { Debug.Log(actionTag + " isn't defined in SpecialActions_Cutscene_Handler."); }
			break;
		}
//	}

	IEnumerator NextScene() {
		GameManager.UIManager.LockScreen();
		DontDestroyOnLoad(gameObject);
		ScreenFader.FadeOut();
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene("Scenes/Development/WorldScene");
		ScreenFader.FadeIn();
		yield return new WaitForSeconds(1f);
		Sadie = GameObject.Find("Sadie");
		GameManager.UIManager.UnlockScreen();
		NextInteraction(next);
	}
}
