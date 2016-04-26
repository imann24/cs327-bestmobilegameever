using UnityEngine;
using System.Collections.Generic;

public class NoahMove : MonoBehaviour {

	private Vector3 destination;
	private NavMeshAgent navAgent;

    private Interactable interactor;
    private List<Interaction> interactionList;
    public Vector2 interactionPos;
    public bool isInteractionPending = false;
    private float minDistanceToInteract;
    private Vector2 lastPosition;

	private Animator anim;
    // Use this for initialization
    void Start () {
		anim = GetComponent<Animator> ();
		navAgent = GetComponent<NavMeshAgent> ();
	}

    // Update is called once per frame
    void Update() {
        if (isInteractionPending) {
            if (Input.GetMouseButtonDown(0)) {
                isInteractionPending = false;
                GameManager.InventoryManager.ReturnSelected();
            }
            else {
                Vector2 currentPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);
                float speed = (lastPosition - currentPos).magnitude * Time.deltaTime;
                lastPosition = currentPos;

                if (speed < 0.00001f) {
                    float distance = (currentPos - interactionPos).sqrMagnitude;
                    if (distance <= minDistanceToInteract) {
                        InteractionManager.HandleInteractionList(interactor, interactionList);
                        isInteractionPending = false;
                    }
                }
            }
        }

		if (anim != null) {
			anim.SetBool ("Walking", navAgent.hasPath);
		}
    }

    public void GoTo(Vector3 targetPoint){
		navAgent.SetDestination(targetPoint);
	}

    public void GoToInteraction(Vector2 pos, Interactable i, List<Interaction> iList) {
        interactionPos = pos;
        interactor = i;
        interactionList = iList;
        isInteractionPending = true;
        lastPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);

        foreach (Interaction interaction in interactionList) {
            minDistanceToInteract = Mathf.Max(0.1f, interaction.iMaxDist);
        }

        NoahNavPlane navFloor = (NoahNavPlane)FindObjectOfType (typeof(NoahNavPlane));
		if (interactionPos.x > transform.position.x) {
			if (navFloor.flipped) {
				navFloor.Flip ();
			}
		} else {
			if (!navFloor.flipped) {
				navFloor.Flip ();
			}
		} 
        GoTo(new Vector3(interactionPos.x, gameObject.transform.position.y, interactionPos.y));
    }
}
