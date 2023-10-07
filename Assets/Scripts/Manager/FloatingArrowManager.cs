using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingArrowManager : MonoBehaviour
{
    public static FloatingArrowManager Inst { get; private set; }

    public GameObject ReginaldArrow;
    public GameObject SantiagoArrow;
    public GameObject LazloArrow;
    public GameObject BrinleyArrow;
    public GameObject SaanviArrow;

    private Dictionary<string, GameObject> arrowDict = new Dictionary<string, GameObject>();

	private void Awake() {
        Inst = this;
	}

	public void ActivateFloatingArrow(string name, bool val = true) {
        if (arrowDict.ContainsKey(name)) {
            arrowDict[name].SetActive(val);
		}
        else {
            Debug.Log(name + " doesn't exist in arrow dict");
		}
	}

}
