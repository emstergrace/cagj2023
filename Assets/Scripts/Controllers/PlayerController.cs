using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Inst;

    [SerializeField] private MovementController playerMovement = null; public MovementController MoveController { get { return playerMovement; } }

	[Header("Coffees")]
	[SerializeField] private GameObject Coffee = null;
	[SerializeField] private GameObject HotChocolate = null;
	[SerializeField] private GameObject Matcha = null;
	[SerializeField] private GameObject Pumpkin = null;

	private bool holdingCoffee = false; 
	
	public void HoldCoffee(bool val) { 
		holdingCoffee = val; 
		playerMovement.animator.SetBool("HoldingCoffee", val); 
		if (val) {
			switch (CoffeeManager.Inst.CurrentCoffee.Brew) {
				case CoffeeManager.BrewType.Coffee:
					Coffee.SetActive(true);
					break;

				case CoffeeManager.BrewType.Cocoa:
					HotChocolate.SetActive(true);
					break;

				case CoffeeManager.BrewType.Matcha:
					Matcha.SetActive(true);
					break;

				case CoffeeManager.BrewType.Pumpkin:
					Pumpkin.SetActive(true);
					break;
			}
		}
		else {
			Coffee.SetActive(false);
			HotChocolate.SetActive(false);
			Matcha.SetActive(false);
			Pumpkin.SetActive(false);
		}
	}

	private void Awake() {
        Inst = this;
	}

    void FixedUpdate() {
		if (!CoffeeBrewUI.Inst.IsBrewing && !ConversationManager.Instance.IsConversationActive) {
			Vector2 direction = new Vector2(-1f * Input.GetAxisRaw("Horizontal"), -1f * Input.GetAxisRaw("Vertical"));
			bool sprinting = Input.GetKey(KeyCode.LeftShift) && !holdingCoffee;
			MoveController.Move(direction, sprinting);
			if (direction != Vector2.zero)
				AudioManager.Inst.Walk();
		}
		else {
			MoveController.Move(Vector2.zero);
		}
	}
}
