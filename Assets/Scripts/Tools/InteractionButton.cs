using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;

class InteractionButton : MonoBehaviour{
	public Interactable interactor;
	public Interaction interaction;

	public void CompleteInteraction(){
		GameManager.InteractionManager.ClearInteractions ();
		InteractionManager.CompleteInteraction (interactor, interaction);
	}
}









