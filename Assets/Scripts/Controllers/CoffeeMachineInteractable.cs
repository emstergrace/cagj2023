using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachineInteractable : Interactable
{
	public override void Interact() {
		CoffeeBrewUI.Inst.ActivateCoffeemachine();
	}
}
