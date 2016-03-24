using UnityEngine;
using System.Collections;

public class CreditsController : MonoBehaviour {

	public string CreditsFilePath = "Text/Credits";

	public Transform CreditsRect;
	public GameObject CreditGroupPrefab;

	void Start () {
		SetCredits(
			GenerateCredits()
		);
	}

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


	public void SetCredits (CreditGroup[] CreditGroups) {
		ClearCredits();

		foreach (CreditGroup creditGroup in CreditGroups) {
			AddCreditGroup(creditGroup);
		}
	}

	public void ClearCredits () {
		for (int i = 0; i < CreditsRect.childCount; i++) {
			Destroy(CreditsRect.GetChild(i));
		}
	}
		
	public void AddCreditGroup (CreditGroup creditGroup) {
		Transform visualCreditGroup = ((GameObject) Instantiate(CreditGroupPrefab)).transform;

		VisualCreditGroup visualCreditGroupController = visualCreditGroup.GetComponent<VisualCreditGroup>();

		visualCreditGroup.SetParent(CreditsRect);

		visualCreditGroup.localScale = new Vector3(1, 1, 1);

		visualCreditGroupController.Set(creditGroup);
	}

}
