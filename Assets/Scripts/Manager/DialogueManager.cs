using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Inst { get; private set; } = null;

	private NodeCanvas loadedDialogue = null;
	private Node currentNode = null;

	[SerializeField] private GameObject dialogueGO = null;
	[SerializeField] private DialogueWindow leftAlignedDialogue = null;
	[SerializeField] private DialogueWindow rightAlignedDialogue = null;

	private DialogueWindow chosenWindow = null;
	public bool IsTalking = false;

	public Action FinishedDialogue;

	private void Awake() {
        Inst = this;
		chosenWindow = leftAlignedDialogue;
		dialogueGO.SetActive(false);
	}

	public void InitiateDialogue(NodeCanvas convo) {
		if (!IsTalking) {
			loadedDialogue = convo;
			currentNode = loadedDialogue.root;
			dialogueGO.SetActive(true);
			chosenWindow.gameObject.SetActive(false);
			if (currentNode is DialogueNode) {
				if (((DialogueNode)currentNode).alignment == Node.Alignment.LeftAligned) {
					chosenWindow = leftAlignedDialogue;
				}
				else {
					chosenWindow = rightAlignedDialogue;
				}
				chosenWindow.gameObject.SetActive(true);
				chosenWindow.SetName(((DialogueNode)currentNode).actorName);
				chosenWindow.SetText(((DialogueNode)currentNode).actorSays);
				chosenWindow.SetFace(((DialogueNode)currentNode).actorSprite);
				StartCoroutine(TimerDelay());
			}
			else {
				//placeholder
			}
		}
	}

	IEnumerator TimerDelay() {
		yield return null;
		IsTalking = true;
	}

	private void Update() {
		if (IsTalking && dialogueGO != null && dialogueGO.activeSelf && currentNode != null && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))) {
			if (leftAlignedDialogue.writingCorout != null || rightAlignedDialogue.writingCorout != null) {
				leftAlignedDialogue.StopWritingCorout();
				rightAlignedDialogue.StopWritingCorout();
			}
			else {
				currentNode = currentNode.nextNode;
				if (currentNode is DialogueNode) {
					NextMessage();
				}
				else {
					// Placeholder
				}
			}
		}
	}

	private void NextMessage() {
		chosenWindow.gameObject.SetActive(false);
		if (currentNode == null) {
			IsTalking = false;
			FinishedDialogue?.Invoke();
			//dialogueGO.SetActive(false);
			return;
		}

		if (((DialogueNode)currentNode).alignment == Node.Alignment.LeftAligned) {
			chosenWindow = leftAlignedDialogue;
		}
		else {
			chosenWindow = rightAlignedDialogue;
		}

		chosenWindow.gameObject.SetActive(true);
		chosenWindow.SetName(((DialogueNode)currentNode).actorName);
		chosenWindow.SetText(((DialogueNode)currentNode).actorSays);
		chosenWindow.SetFace(((DialogueNode)currentNode).actorSprite);
	}
}
