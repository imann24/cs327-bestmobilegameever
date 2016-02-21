using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

//Remove highscore and Money for things we want to save and record in playerData class

public class SaveLoad {

	public void Save(PlayerData data){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/Save.sav");

		bf.Serialize (file, data);
		file.Close ();
	}

	public PlayerData Load(){

		if (File.Exists (Application.persistentDataPath + "/Save.sav")) {
		
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/Save.sav", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close();

			return data;

		} else {

			return new PlayerData();

		}

	}
	public void ClearSave(){

		if (File.Exists (Application.persistentDataPath + "/Save.sav")) {

			File.Delete( Application.persistentDataPath + "/Save.sav");

		}

	}
}

[Serializable]
public class PlayerData
{
	public PlayerData () {
		Progress = new ProgressTracker();
	}

	public ProgressTracker Progress;
	public Dictionary<string, bool> someData;
//	public float HighScore;
//	public float Money;
}

