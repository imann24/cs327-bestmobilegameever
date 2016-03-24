using UnityEngine;
using System.Collections;

public class CreditsController : MonoBehaviour {
	public string CreditsFilePath = "Text/Credits";

	public void BackToOptionsMenu () {
        SceneController.LoadMainMenu();
    }

	public CreditGroup[] GenerateCredits () {
		string[,] creditsAsStrings = CSVUtil.ReadCSV(CreditsFilePath);

		CreditGroup[] allCredits = new CreditGroup[creditsAsStrings.GetLength(0)];

		for (int i = 0; i < allCredits.Length; i++) {
			allCredits[i] = new CreditGroup(
				ArrayUtil.RemoveEmptyElements(
					ArrayUtil.CopyColumn(creditsAsStrings, i)
				)
			);
		}

		return allCredits;
	}
}
