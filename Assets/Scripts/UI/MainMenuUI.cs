using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
	[SerializeField] private GameObject creditsWindow = null;

	public void PlayButton() {
		AudioManager.Inst.PlaySound("playbutton");
		SceneManager.LoadScene("MainScene");
	}

	public void SettingsButton() {
		AudioManager.Inst.PlaySound("selectbutton");
		creditsWindow.SetActive(false);
	}

	public void Credits() {
		AudioManager.Inst.PlaySound("selectbutton");
		creditsWindow.SetActive(true);
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			CloseWindows();
		}
	}

	public void CloseWindows() {
		AudioManager.Inst.PlaySound("backbutton");
		creditsWindow.SetActive(false);
	}
}
