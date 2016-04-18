using UnityEngine;
using System.Collections;

public class PSSceneUtil {

	public static bool InGame (int loadNumber) {
		return InGame((PSScene) loadNumber);
	}

	public static bool InGame (PSScene scene) {
		return scene == PSScene.MainGame ||
			scene == PSScene.TutorialScene;
	}

}
