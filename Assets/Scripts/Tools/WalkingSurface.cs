using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class WalkingSurface : MonoBehaviour, IPointerClickHandler{
	public void OnPointerClick (PointerEventData eventData)
	{
		Vector3 walkHere = eventData.pointerPressRaycast.worldPosition;
		GameManager.Instance.playerCharacter.GetComponent<Movement> ().MoveTo (walkHere);
	}
}
