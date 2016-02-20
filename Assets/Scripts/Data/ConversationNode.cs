/*
 * Author: Isaiah Mann
 * Description: Used to store a single node of conversation
 */

[System.Serializable]
public class ConversationNode {
	public string SpeakerName;
	public string Text;
	public string[] Responses;
	public ConversationNode Next;

	public bool HasNext {
		get {
			return ! (Next == null);
		}
	}

	public ConversationNode (string speakerName, string text, params string[] responses) {
		this.SpeakerName = speakerName;
		this.Text = text;
		this.Responses = responses;
	}

	public ConversationNode (string speakerName, string text, ConversationNode next, params string[] responses) {
		this.SpeakerName = speakerName;
		this.Text = text;
		this.Responses = responses;
		SetNext(next);
	}

	public void SetNext (ConversationNode node) {
		this.Next = node;
	}

	// Counts number of nodes until end
	public int Count () {
		if (HasNext) {
			return Next.Count() + 1;
		} else {
			return 1;
		}
	}

	public override string ToString () {
		return string.Format (
			"[ConversationNode: Speaker Name={0}, Text={1}, Responses={2}]",
			SpeakerName,
			Text,
			ArrayUtil.ToString(Responses)
		);
	}
}