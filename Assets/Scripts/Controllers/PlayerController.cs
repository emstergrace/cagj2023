using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Inst;

    [SerializeField] private MovementController playerMovement = null; public MovementController MoveController { get { return playerMovement; } }
    

	private void Awake() {
        Inst = this;
	}

    void FixedUpdate() {
		if (!CoffeeBrewUI.Inst.IsBrewing && !ConversationManager.Instance.IsConversationActive) {
			Vector2 direction = new Vector2(-1f * Input.GetAxisRaw("Horizontal"), -1f * Input.GetAxisRaw("Vertical"));
			bool sprinting = Input.GetKey(KeyCode.LeftShift);
			MoveController.Move(direction, sprinting);
			if (direction != Vector2.zero)
				AudioManager.Inst.Walk();
		}
	}
}
