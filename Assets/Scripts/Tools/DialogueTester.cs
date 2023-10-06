using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialogueTester : MonoBehaviour
{

    public NPCConversation testConvo;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) {
            if (!ConversationManager.Instance.IsConversationActive) {
                ConversationManager.Instance.StartConversation(testConvo);
			}
            else {
                if (Input.GetKeyDown(KeyCode.UpArrow)) {
                    ConversationManager.Instance.SelectPreviousOption();
				}
                else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                    ConversationManager.Instance.SelectNextOption();
				}
                else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) {
                    ConversationManager.Instance.PressSelectedOption();
				}
			}
		}
    }
}
