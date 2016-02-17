using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

//Remove highscore and Money for things we want to save and record in playerData class

public class SaveLoad {
	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/Save.sav");

		PlayerData data = new PlayerData ();
//		data.HighScore = GameManager.Instance.HighScore;
//		data.Money = GameManager.Instance.Money;

		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load(){
		if (File.Exists (Application.persistentDataPath + "/Save.sav")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/Save.sav", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize(file);


//			GameManager.Instance.HighScore = data.HighScore;
//			GameManager.Instance.Money = data.Money;
			file.Close();
		}

	}
	public void ClearSave(){
		if (File.Exists (Application.persistentDataPath + "/Save.sav")) {
			File.Delete( Application.persistentDataPath + "/Save.sav");
//			GameManager.Instance.HighScore = 0;
//			GameManager.Instance.Money = 0;
		}

	}
}
[Serializable]
class PlayerData
{
	public Dictionary<string, bool> someData;
//	public float HighScore;
//	public float Money;
}

