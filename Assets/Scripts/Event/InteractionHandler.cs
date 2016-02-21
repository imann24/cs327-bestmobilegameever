using UnityEngine;
using System.Collections;

public class InteractionHandler : MonoBehaviour {

	ConversationDisplayController _conversation;

	ProgressTracker _progress;

	// Use this for initialization
	void Start () {
		init();
	}

	void OnDestroy () {
		unsubscribeEvents();
	}

	void subscribeEvents () {
		EventController.OnInteractionEvent += handleInteraction;
	}

	void unsubscribeEvents () {
		EventController.OnInteractionEvent -= handleInteraction;
	}

	void handleInteraction (InteractionID interaction) {
		switch (interaction) {

		case InteractionID.FirstMate:
			handleFirstMateInteraction();
			break;

		}
	}

	void handleFirstMateInteraction () {
		if (!_progress.IntroducedToFirstMate) {
		
			_conversation.StartConversation (
				ConversationXMLFileList.FIRST_MATE_INTRO
			);

			_progress.IntroducedToFirstMate = true;

			WorldController.Instance.Save();

		} else if (!_progress.PickedUpMop) {

			_conversation.StartConversation (
				ConversationXMLFileList.FIRST_MATE_MOP_NOT_YET_FOUND
			);

		} else if (!_progress.PickedUpPaint) {

			_conversation.StartConversation (
				ConversationXMLFileList.FIRST_MATE_MOP_FOUND
			);

		} else {

			_conversation.StartConversation (
				ConversationXMLFileList.FIRST_MATE_PORTRAIT_PAINTED
			);

		}
	}

	void init () {
		subscribeEvents();
		_progress = WorldController.Instance.SaveFile.Progress;
		_conversation = ConversationDisplayController.Instance;
	}
}
