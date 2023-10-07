using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterInteractable : Interactable
{
	public override void Interact() {
		CoffeeManager.Inst.FinishDay();
	}
}
