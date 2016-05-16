using UnityEngine;
using System.Collections;

public class SpecialActions_Swabbie : SpecialActions {
	const string GIVE_UP_MOP_ANIMATOR_KEY = "GiveAwayMop";
    public GameObject Ink_Puddle;
    public GameObject Waypoint_Ink;

    public override void DoSpecialAction(string actionTag) {
		AudioController audio;
        switch (actionTag) {
		case "SwabbieFlee":
            Vector3 pos = new Vector3(Waypoint_Ink.transform.position.x, Waypoint_Ink.transform.position.y, gameObject.transform.position.z);
            GameObject ink = (GameObject)Instantiate(Ink_Puddle, pos, Quaternion.identity);
            ink.name = Ink_Puddle.name;
            Ink_Puddle = ink;
            Invoke("inkObstacle", 1f);
			Invoke ("destroyMe", 3f);
            break;
		case "StopMopping":
			audio = GameObject.Find ("AudioController").GetComponent<AudioController> ();
			audio.SwabbieRun ();
			break;
		case "SwabbieSpeech":
			audio = GameObject.Find ("AudioController").GetComponent<AudioController> ();
			audio.VoiceEffect ("SwabbieSpeech");
			break;
		case GIVE_UP_MOP_ANIMATOR_KEY:
			GetComponent<Animator>().SetTrigger(actionTag);
			break;
        }
    }

    private void inkObstacle() {
        Ink_Puddle.transform.GetChild(1).gameObject.SetActive(true);
    }

    private void destroyMe() {
        Destroy(gameObject);
    }
}