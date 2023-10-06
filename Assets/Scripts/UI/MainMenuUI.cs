using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
	[SerializeField] private GameObject creditsWindow = null;

	[SerializeField] private SoundMenu soundMenu = null;

	public void PlayButton() {
		AudioManager.Inst.PlaySound("Button");
		SceneManager.LoadScene("EnvironmentTestScene");
	}

	public void SettingsButton() {
		AudioManager.Inst.PlaySound("Button");
		creditsWindow.SetActive(false);
		soundMenu.OpenMenu();
	}

	public void Credits() {
		AudioManager.Inst.PlaySound("Button");
		soundMenu.CloseMenu();
		creditsWindow.SetActive(true);
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			CloseWindows();
		}
	}

	public void CloseWindows() {
		AudioManager.Inst.PlaySound("Button");
		soundMenu.CloseMenu();
		creditsWindow.SetActive(false);
	}

	public void LowerMusic() {
		soundMenu.LowerMusic();
	}

	public void RaiseMusic() {
		soundMenu.RaiseMusic();
	}

	public void LowerSound() {
		soundMenu.LowerSound();
	}

	public void RaiseSound() {
		soundMenu.RaiseSound();
	}
}
