using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class PauseMenu : BaseMenu
{
	[SerializeField] private GameObject PauseCanvas = null;

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (!ConversationManager.Instance.IsConversationActive && !CoffeeBrewUI.Inst.IsBrewing) {
				PauseCanvas.SetActive(!PauseCanvas.activeSelf);
			}
		}	
	}

	public override void OpenMenu() {
		throw new System.NotImplementedException();
	}

	public override void CloseMenu() {
		throw new System.NotImplementedException();
	}
}
