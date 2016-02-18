using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VisualConversationDisplayer {

	public ConversationInterface CurrentConvo;
	public GameObject Char1Sprite;
	public GameObject Char2Sprite;
	public GameObject BackgroundSprite;

	public VisualConversationDisplayer (ConversationInterface c, Sprite x, Sprite y, Sprite z){
		CurrentConvo = c;
		Char1Sprite.GetComponent<SpriteRenderer>().sprite = x;
		Char2Sprite.GetComponent<SpriteRenderer>().sprite = y;
		BackgroundSprite.GetComponent<SpriteRenderer>().sprite = z;
	}

	public void BeginConvo(){

	}

	public void AdvanceConvo(){

	}

	public void EndConvo(){

	}


}
