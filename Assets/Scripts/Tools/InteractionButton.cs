using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InteractionButton : MonoBehaviour {

	public Interactable buttonInteractor;
	public Interaction buttonInteraction;

	public static void Generate(Interactable interactor, Interaction interaction){
		Debug.Log ("Showing Interaction Panel");
		UIManager.Instance.ShowInteractionPanel ();
		Transform panelTransform = UIManager.Instance.transform.FindChild ("InteractionPanel/InteractionTextPanel");
		GameObject newButton = Instantiate(Resources.Load<GameObject>("Prefabs/InteractionButton"));
		newButton.GetComponentInChildren<Text> ().text = interaction.iText;
		newButton.GetComponent<InteractionButton> ().buttonInteraction = interaction;
		newButton.GetComponent<InteractionButton> ().buttonInteractor = interactor;
		newButton.transform.SetParent(panelTransform);
	}

	public void CompleteInteraction(){
		InteractionManager.InteractionButtonPressed (buttonInteractor, buttonInteraction);
	}
}
