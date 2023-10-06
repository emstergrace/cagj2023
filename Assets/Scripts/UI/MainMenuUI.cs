using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
	[SerializeField] private GameObject creditsWindow = null;

	public void PlayButton() {
		AudioManager.Inst.PlaySound("button");
		SceneManager.LoadScene("MainScene");
	}

	public void SettingsButton() {
		AudioManager.Inst.PlaySound("button");
		creditsWindow.SetActive(false);
	}

	public void Credits() {
		AudioManager.Inst.PlaySound("button");
		creditsWindow.SetActive(true);
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			CloseWindows();
		}
	}

	public void CloseWindows() {
		AudioManager.Inst.PlaySound("button");
		creditsWindow.SetActive(false);
	}
}
