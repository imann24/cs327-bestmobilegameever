using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VisualConversationDisplayer {

	public ConversationInterface CurrentConvo;
	public GameObject Char1Sprite;
	public GameObject Char2Sprite;
	public GameObject BackgroundSprite;
	public GameObject DialogButton;
	public GameObject CharName;

	public VisualConversationDisplayer (ConversationInterface c, Sprite x, Sprite y, Sprite z, Button b, string n){
		CurrentConvo = c;
		Char1Sprite.GetComponent<SpriteRenderer>().sprite = x;
		Char2Sprite.GetComponent<SpriteRenderer>().sprite = y;
		BackgroundSprite.GetComponent<SpriteRenderer>().sprite = z;
		DialogButton = b;
		CharName = n;

	}

	public void BeginConvo(){
		//create UI objects, set parent to canvas, set anchors, set positions
		GameObject canvas = Instantiate(Resources.Load("CanvasPF")) as GameObject;
		GameObject inSceneChar1 = Instantiate (Char1Sprite);
		//GameObject inSceneChar2 = Instantiate (Char2Sprite);
		GameObject inSceneBackground = Instantiate (BackgroundSprite);
		GameObject inSceneButton = Instantiate (DialogButton);
		GameObject charNameText = Instantiate(Resources.Load("CharacterName")) as GameObject;
		GameObject dialogText = Instantiate(Resources.Load ("DialogText")) as GameObject;
		inSceneChar1.transform.SetParent(canvas, false);
		//inSceneChar2.transform.SetParent(canvas, false);
		inSceneBackground.transform.SetParent(canvas, false);
		charNameText.transform.SetParent(canvas, false);
		dialogText.transform.SetParent(canvas, false);
		inSceneButton.transform.SetParent (canvas, false);
		RectTransform inSceneChar1RT = inSceneChar1.GetComponent<RectTransform>(); 
		//RectTransform inSceneChar2RT = inSceneChar2.GetComponent<RectTransform>();   
		RectTransform inSceneBackgroundRT = inSceneBackground.GetComponent<RectTransform>();   
		RectTransform charNameTextRT = charName.GetComponent<RectTransform>();   
		RectTransform dialogTextRT = dialogText.GetComponent<RectTransform>(); 

		inSceneChar1RT.anchorMin = new Vector2 (0.7541881,0.02033898);
		inSceneChar1RT.anchorMax = new Vector2 (1,1);

		//inSceneChar2RT.anchorMin = new Vector2 (0,0);
		//inSceneChar2RT.anchorMax = new Vector2 (.242,1);

		inSceneBackgroundRT.anchorMin = new Vector2 (0,0.02033898);
		inSceneBackgroundRT.anchorMax = new Vector2 (1,.339);

		charNameTextRT.anchorMin = new Vector2 (0,.189);
		charNameRT.anchorMax = new Vector2 (.5,.319);

		dialogTextRT.anchorMin = new Vector2 (.5,0);
		dialogTextRT.anchorMax = new Vector2 (1,.319);

		inSceneButton.anchorMin = new Vector2 (.7055,0);
		inSceneButton.anchorMax = new Vector2 (1,.189);

		charNameText.GetComponent<Text> ().text = CharName;
		dialogText.GetComponent<Text> ().text = CurrentConvo.GetFirstDialogue ();
	
	}

	public void AdvanceConvo(){
		dialogText.GetComponent<Text> ().text = CurrentConvo.AdvanceDialogue ();
	}

	public void EndConvo(){
		Destroy (Canvas);
	}


}
