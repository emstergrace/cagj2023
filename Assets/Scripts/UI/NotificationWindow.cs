using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationWindow : MonoBehaviour
{
    public static NotificationWindow Inst { get; private set; } = null;
    [SerializeField] private GameObject notificationBox = null;
    [SerializeField] private TextMeshProUGUI textWindow = null;

    private bool closingBox = false;

	private void Awake() {
        Inst = this;
	}

	private Coroutine notifCorout = null;
    public void AddNotification(string val, float showTime = 1f) {
        if (val.Length == 0) return;
        if (notifCorout != null) {
            StopCoroutine(notifCorout);
            StartCoroutine(CloseBox());
		}
        notifCorout = StartCoroutine(NotifyCoroutine(val, showTime));
	}

    IEnumerator CloseBox() {
        float time = 0f;
        closingBox = true;
        while (time < 0.5f) {
            time += Time.deltaTime;
            notificationBox.transform.localScale = Vector3.one * Mathf.Lerp(1f, 0f, time / 0.5f);
            yield return null;
		}
        closingBox = false;
        notificationBox.SetActive(false);
	}

    IEnumerator NotifyCoroutine(string val, float showTime) {
        while (closingBox) {
            yield return null;
        }

        notificationBox.SetActive(true);
        notificationBox.transform.localScale = Vector3.zero;
        textWindow.text = val;
        float time = 0f;
        while (time < showTime) {
            notificationBox.transform.localScale = Vector3.one * Mathf.Lerp(0f, 1f, time / 0.5f);
            time += Time.deltaTime;
            yield return null;
		}
        yield return StartCoroutine(CloseBox());
        notifCorout = null;
	}
}
