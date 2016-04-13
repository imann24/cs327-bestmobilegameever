using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public static PlayerController Instance
	{
		get
		{
			return instance;
		}
	}
	private static PlayerController instance = null;

	void Awake (){
		if(instance){
			PlayerController.Instance.transform.position = gameObject.transform.position;
			PlayerController.Instance.transform.localScale = gameObject.transform.localScale;
			Camera.main.GetComponent<CameraFollower> ().target = PlayerController.Instance.transform;
			DestroyImmediate(gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
