using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VisualCreditGroup : MonoBehaviour {

	public Text Title;
	public Text AllCredits;

	public void Set (CreditGroup credit) {
		Title.text = credit.Title;

		AllCredits.text = credit.GetAllCredits();
	}

}