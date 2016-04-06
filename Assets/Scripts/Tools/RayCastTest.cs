using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class RayCastTest : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler {
	#region IPointerDownHandler implementation
	public void OnPointerDown (PointerEventData eventData)
	{
		Debug.Log ("Down");
	}
	#endregion

	#region IPointerUpHandler implementation

	public void OnPointerUp (PointerEventData eventData)
	{
		Debug.Log ("Up");
	}

	#endregion

	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
	{
		Debug.Log ("Clicked");
	}

	#endregion
}
