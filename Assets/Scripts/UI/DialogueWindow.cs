using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speech = null;
    [SerializeField] private TextMeshProUGUI charName = null;
    [SerializeField] private Image actorImage = null;

    private string currentText = "";
    public bool ReadyForText { get; private set; } = false;

    public void SetName(string val) {
        charName.text = val;
    }

    public void ForceSetText(string val) {
        speech.text = val;
    }

    public void SetText(string val) {
        currentText = val;
        WriteCurrentText();
	}

    public void SetFace(Sprite sprite) {
        actorImage.sprite = sprite;
	}

    public Coroutine writingCorout { get; private set; } = null;
    public void WriteCurrentText() {
        writingCorout = StartCoroutine(WriteCharByChar(currentText));
	}

    private IEnumerator WriteCharByChar(string val) {
        ReadyForText = false;
        speech.text = "";
        int index = 0;
        while (index < val.Length) {
            speech.text += val[index];
            index++;
            yield return null;
            yield return null;
		}
        writingCorout = null;
        ReadyForText = true;
	}

    public void StopWritingCorout() {
        if (writingCorout != null)
            StopCoroutine(writingCorout);
        writingCorout = null;
        ForceSetText(currentText);
        ReadyForText = true;
	}
}
