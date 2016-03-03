#define DEBUG

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	[SerializeField]
	GameObject tapToContinue = null;

	public void EnableTapToContinue(Interactable interactor, Interaction interaction){
		tapToContinue.SetActive (true);
		tapToContinue.GetComponent<InteractionButton> ().interactor = interactor;
		tapToContinue.GetComponent<InteractionButton> ().interaction = interaction;
	}

	public void DisableTapToContinue(){
		tapToContinue.SetActive (false);
	}

	public void LockScreen(){
		tapToContinue.SetActive (true);
		tapToContinue.GetComponent<Button> ().enabled = false;
	}

	public void UnlockScreen(){
		tapToContinue.GetComponent<Button> ().enabled = true;
		tapToContinue.SetActive (false);
	}
}
