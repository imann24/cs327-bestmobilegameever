using UnityEngine;
using System.Collections;

[System.Serializable]
public class CharacterClass : MonoBehaviour {

	private static DataTree characterList;

	public string characterName;
	private static string[,] peopleList = new string[2, 5]; // Amount of characters and max number of spites + 1  for each character
	// [0, 0] serves as the name of each other where [0, 1] is the first sprite

	string[] sprites = new string[peopleList.GetLength(1) - 1];
	private SpriteRenderer spriteRenderer;

	bool _spriteLoadingImplemented = false;

	void Awake () {
		initCharacterList();
	}

	void Start(){
		GetPeople ();
		 
		//indivdual sprite list for character via name
		sprites = GetSprites (name);
		spriteRenderer = GetComponent<SpriteRenderer> ();

		if (_spriteLoadingImplemented) {
			Sprite currentSprite = (Sprite) Resources.Load (sprites [0], typeof(Sprite)); 
			spriteRenderer.sprite = currentSprite;
		}
	}

	public void GetPeople(){  //Initializes list of people and their sprites
		for (int i = 0; i < peopleList.GetLength(0); i++) { 
			peopleList [i, 0] = characterList [i].Value.ToString ();
			for (int j = 0; j < peopleList.GetLength(1) - 1; j++) {
				if (characterList [i] [j] != null) {
					peopleList [i, j + 1] = characterList [i] [j] [0].Value.ToString ();
				}
			}
		}
	}

	public string[] GetSprites(string name){ //Returns list of sprites for a specific character name
		for (int p = 0; p < peopleList.GetLength(0); p++){
			if (peopleList[p, 0] == name) {
				string[] sprites = new string[peopleList.GetLength (1) - 1];
				for (int i = 0; i < sprites.Length; i++) {
					sprites [i] = peopleList [p, i + 1];
				}
				return sprites;
			}
		}
		return null;
	}

	void initCharacterList () {
		if (characterList == null) {
			characterList = DataUtil.ParseXML("Text/CharacterList");
		}
	}
}
