using UnityEngine;
using System.Collections;

public class DevelopmentSceneListController : MonoBehaviour {

	public void BackToMainMenu () {
		SceneController.LoadMainMenu();
	}

	public void LoadMovementDemo () {
		SceneController.LoadMovementDemoScene();
	}

	public void LoadSpritesDemo () {
		SceneController.LoadCharacterSpritesDemo();
	}
}
