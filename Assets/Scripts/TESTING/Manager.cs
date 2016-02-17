using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

	public GameObject optionsPanel;
	public GameObject optionsButton;
	public GameObject settingsPanel;
	// Use this for initialization
	void Start () {
		Inventory inv = Inventory.Instance;
	}

	public void OpenOptions(){
		optionsPanel.SetActive (true);
		optionsButton.GetComponent<Button> ().enabled = false;
	}

	public void OptionsSave(){
		Debug.Log ("Saved!");
	}

	public void OptionsBack(){
		optionsPanel.SetActive (false);
		optionsButton.GetComponent<Button>().enabled = true;
	}

	public void OptionsQuit(){
		Application.Quit ();
	}

	public void OptionsSettings(){
		optionsPanel.SetActive (false);
		settingsPanel.SetActive (true);
	}

	public void SettingsBack(){
		settingsPanel.SetActive (false);
		optionsPanel.SetActive (true);
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && Inventory.Instance.selectedItem != null) {
			if (false) {
				//Inventory.Instance.ReplaceItem ();
			} else {
				if (Inventory.Instance.selectedItem != null) {
					Inventory.Instance.DropItem ();
				}
			}
		}
	}
}