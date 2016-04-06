using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreditsController : MonoBehaviour {

	public string CreditsFilePath = "Text/Credits";

	public Transform CreditsRect;
	public GameObject CreditGroupPrefab;
	public ScrollRect CreditsScroll;

	public float MoveSpeed = 100;

	bool _creditsAreAutoMoving = true;

	void Start () {
		init();	
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

	public void HaltMovement () {
		_creditsAreAutoMoving = false;
	}
		
	public void CheckForScrollEnd (float scrollBarValue) {
		if (scrollBarValue == 1) {
			_creditsAreAutoMoving = false;
		}
	}

	IEnumerator LerpCredtsLeft (float waitTime = 0.75f) {
		yield return new WaitForSeconds (waitTime);

		while (_creditsAreAutoMoving) {

			CreditsRect.position += Vector3.left * Time.deltaTime * MoveSpeed;

			yield return new WaitForEndOfFrame();
		}
	}

	void init () {

		SetCredits(
			GenerateCredits()
		);

		StartCoroutine(LerpCredtsLeft());
	}
		


}
