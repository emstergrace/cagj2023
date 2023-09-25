using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// all item classes must inherit IInteractable and have Interact function

public interface IInteractable 
{
	public void Interact();
}

public class Interactor : MonoBehaviour
{

	public Transform InteractorSource;
	public float InteractRange;
	public List<GameObject> interactObjs = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
		if (DialogueManager.Inst.IsTalking) return;

		if (Input.GetKeyDown(KeyCode.E) && interactObjs.Count > 0) 
		{
			interactObjs[0].GetComponent<IInteractable>().Interact();
		}
		
    }

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == 6) {
			IInteractable obj = other.GetComponent<IInteractable>();
			if (obj != null && !interactObjs.Contains(other.gameObject)) {
				interactObjs.Add(other.gameObject);
			}
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.gameObject.layer == 6) {
			if (interactObjs.Contains(other.gameObject)) interactObjs.Remove(other.gameObject);
		}
	}


}
