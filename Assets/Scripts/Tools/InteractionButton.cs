using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InteractionButton : MonoBehaviour {

	Interactable buttonInteractor;
	Interaction buttonInteraction;
	List<Interaction> possibleFollowUpInteractions;

	public static void Generate(Interactable interactor, Interaction interaction, List<Interaction> nextInteractions){
		Transform panelTransform = UIManager.Instance.transform.FindChild ("InteractionPanel/InteractionTextPanel");
		GameObject newButton = Instantiate(Resources.Load<GameObject>("Prefabs/InteractionButton"));
		newButton.GetComponentInChildren<Text> ().text = interaction.iText;
		newButton.GetComponent<InteractionButton> ().buttonInteraction = interaction;
		newButton.GetComponent<InteractionButton> ().possibleFollowUpInteractions = nextInteractions;
		newButton.GetComponent<InteractionButton> ().buttonInteractor = interactor;
		newButton.transform.SetParent(panelTransform);
	}

	public void CompleteInteraction(){
		InteractionManager.CompleteInteraction (buttonInteractor, buttonInteraction);
	}
}
