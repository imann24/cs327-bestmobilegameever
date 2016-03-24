/*
 * Author: Isaiah Mann
 * Description: Stores a list of credits
 */
using UnityEngine;
using System.Collections;

public class CreditGroup {

	public string Title;
	public Credit[] Credits;

	public CreditGroup (string title, params string[] credits) {
		this.Title = title;
		this.Credits = generateCredits(credits);
	}

	public CreditGroup (params string[] creditsStartingWithTitle) {
		this.Title = creditsStartingWithTitle[0];
		this.Credits = generateCredits(ArrayUtil.RemoveFirst(creditsStartingWithTitle));
	}

	Credit[] generateCredits (params string[] creditNames) {
		Credit[] credits = new Credit[creditNames.Length];

		for (int i = 0; i < credits.Length; i++) {
			credits[i] = new Credit(creditNames[i]);
		}

		return credits;
	}

	public string GetAllCredits () {
		string allCredits = "";

		for (int i = 0; i < Credits.Length; i++) {
			allCredits += Credits[i].Name;

			if (i < Credits.Length - 1) {
				allCredits += "\n";
			}
		}

		return allCredits;
	}
	public override string ToString () {

		return "[CreditGroup] " + Title + ":\n" +
			ArrayUtil.ToString(Credits);

	}
		
}
