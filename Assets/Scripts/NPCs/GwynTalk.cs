using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class GwynTalk : MonoBehaviour, IInteractable
{
	public NPCConversation convo1;
	public NPCConversation convo2;
	public NPCConversation convo3;

	public void Interact() {
		int random = Random.Range(0, 3);
		switch (random) {
			case 0:
				ConversationManager.Instance.StartConversation(convo1);
				break;
			case 1:
				ConversationManager.Instance.StartConversation(convo2);
				break;
			case 2:
				ConversationManager.Instance.StartConversation(convo3);
				break;
		}
	}
}
