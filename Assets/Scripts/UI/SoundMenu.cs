using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMenu : MonoBehaviour
{
    [SerializeField] private GameObject soundWindow = null;
	[Header("Music")]
	[SerializeField] private List<GameObject> musicBar = new List<GameObject>();
	private float musicVolume = 0.5f;
	private int currentMusicIndex = 4;
	[Header("Sound")]
	[SerializeField] private List<GameObject> soundBar = new List<GameObject>();
	private float soundVolume = 0.5f;
	private int currentSoundIndex = 4;

	private void Start() {
		soundVolume = AudioManager.Sound;
		musicVolume = AudioManager.Music;
		currentMusicIndex = Mathf.RoundToInt((musicVolume - 0.1f) * 10);
		currentSoundIndex = Mathf.RoundToInt((soundVolume - 0.1f) * 10);
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (soundWindow.activeSelf) CloseSoundMenu();
			else OpenSoundMenu();
		}
	}

	public void OpenSoundMenu() {
		soundWindow.SetActive(true);
		for (int i = 0; i < soundBar.Count; i++) {
			if (i <= currentSoundIndex) {
				soundBar[i].SetActive(true);
			}
			else {
				soundBar[i].SetActive(false);
			}
			if (i <= currentMusicIndex) {
				musicBar[i].SetActive(true);
			}
			else {
				musicBar[i].SetActive(false);
			}
		}
	}

	public void CloseSoundMenu() {
		soundWindow.SetActive(false);
	}

	public void LowerMusic() {
		AudioManager.Inst.PlayButton();
		if (currentMusicIndex >= 0) {
			musicBar[currentMusicIndex].SetActive(false);
			currentMusicIndex--;
			musicVolume = (currentMusicIndex + 1) / 10f;
			AudioManager.Inst.SetMusicLevel(musicVolume);
		}
	}

	public void RaiseMusic() {
		AudioManager.Inst.PlayButton();
		if (currentMusicIndex < 9) {
			currentMusicIndex++;
			musicBar[currentMusicIndex].SetActive(true);
			musicVolume = (currentMusicIndex + 1) / 10f;
			AudioManager.Inst.SetMusicLevel(musicVolume);
		}
	}

	public void LowerSound() {
		AudioManager.Inst.PlayButton();
		if (currentSoundIndex >= 0) {
			soundBar[currentSoundIndex].SetActive(false);
			currentSoundIndex--;
			soundVolume = (currentSoundIndex + 1) / 10f;
			AudioManager.Inst.SetSoundLevel(soundVolume);
		}
	}

	public void RaiseSound() {
		AudioManager.Inst.PlayButton();
		if (currentSoundIndex < 9) {
			currentSoundIndex++;
			soundVolume = (currentSoundIndex + 1) / 10f;
			soundBar[currentSoundIndex].SetActive(true);
			AudioManager.Inst.SetSoundLevel(soundVolume);
		}
	}
}
