using UnityEngine;
using System.Collections;

public class OrangeGuyController : MonoBehaviour {


	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	public void OrangeIn () {
		anim.SetTrigger ("OrangeIn");
	}

	public void OrangeOut(){
		anim.SetTrigger ("OrangeOut");
	}
}
