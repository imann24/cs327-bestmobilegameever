using UnityEngine;
using System.Collections;

public class CharacterClass : MonoBehaviour {

	private static DataTree characterList = DataUtil.ParseXML("Text/CharacterList");

	string[,] peopleList = new string[2, 5]; // Amount of characters and max number of spites + 1  for each character
	// [0, 0] serves as the name of each other where [0, 1] is the first sprite

	void Start(){
		GetPeople ();
		 
		//indivdual sprite list for character via name
		string[] person = new string[peopleList.GetLength(1) - 1];
		person = GetSprites ("Bob"); 


		foreach (string sprite in person) {
			Debug.Log (sprite);
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

}
