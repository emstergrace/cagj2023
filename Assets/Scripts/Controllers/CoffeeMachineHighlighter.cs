using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachineHighlighter : MonoBehaviour
{

	public Material outlineMaterial;
	public MeshRenderer coffeeMachineMR;

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject == PlayerController.Inst.gameObject) {
			coffeeMachineMR.materials = new Material[] { coffeeMachineMR.materials[0], outlineMaterial };
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.gameObject == PlayerController.Inst.gameObject) {
			coffeeMachineMR.materials = new Material[] { coffeeMachineMR.materials[0] };
		}
	}
}
