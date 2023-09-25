using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Inst { get; private set; }

	[SerializeField] private AudioListener playerListener;
	[Header("Sounds")]
	[SerializeField] private AudioSource buttonClick = null; public void PlayButton() { if (buttonClick != null) buttonClick.Play(); }
	[SerializeField] private AudioSource walkNoise = null;
	public void Walk() {
		if (walkNoise != null && !walkNoise.isPlaying) {
			footIndex = UnityEngine.Random.Range(0, footNoises.Count);
			walkNoise.clip = footNoises[footIndex];
			walkNoise.Play();
		}
	}
	public void Sprint(bool val) { if (val && walkNoise.pitch != 2f) walkNoise.pitch = 2f; else if (!val && walkNoise.pitch != 1f) walkNoise.pitch = 1f; }
	[SerializeField] private List<AudioClip> footNoises = new List<AudioClip>();
	private int footIndex = 0;
	[SerializeField] private List<AudioSource> sceneAudio = new List<AudioSource>();
	private static float soundVolume = 0.5f; public static float Sound { get { return soundVolume; } }
	public Action<float> NewSoundVolume;

	[Header("Music")]
	[SerializeField] private AudioSource menuMusic = null;
	[SerializeField] private AudioSource mainTheme = null;
	private static float musicVolume = 0.5f; public static float Music { get { return musicVolume; } }

	private void Awake() {
		Inst = this;
	}

	private void Start() {

		SetSoundLevel(soundVolume);
		SetMusicLevel(musicVolume);
	}

	public void SubscribeSFXAudio(AudioSource source) {
		sceneAudio.Add(source);
		source.volume = soundVolume;
	}
	public void UnsubscribeSFXAudio(AudioSource source) {
		sceneAudio.Remove(source);
	}

	public void SetSoundLevel(float level) {
		soundVolume = level;
		foreach(AudioSource source in sceneAudio) {
			source.volume = soundVolume;
		}
		if (buttonClick != null)
		buttonClick.volume = level;
		if (walkNoise != null)
		walkNoise.volume = level / 2;
		NewSoundVolume?.Invoke(level);
	}

	public void SetMusicLevel(float level) {
		musicVolume = level;
		if (menuMusic != null) menuMusic.volume = musicVolume;
		if (mainTheme != null) mainTheme.volume = musicVolume;
	}

	public void PlayMainTheme() {
		mainTheme.Play();
		playerListener.gameObject.SetActive(true);
	}
}
