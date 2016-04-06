using UnityEngine;
using System.Collections;

//reveal the white rag object when the player
public class SpecialActions_Bottle : SpecialActions 
{

	// Use this for initialization
	public GameObject Rag; 

	public override void DoSpecialAction (string actionTag)
	{
		bool destroy = false;
		if(actionTag.Equals("RevealWhiteRagObject"))
		{
			destroy=true;
			Instantiate(Rag,transform.position,Quaternion.identity);
			return;
		}
		if(destroy)
		{
			Destroy(gameObject);
			return;
		}
	}
}
