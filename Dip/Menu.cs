﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	public GameObject mainMenuHolder;
	public GameObject optionsMenuHolder;

	public Slider[] volumeSliders;
	public Toggle[] resolutionTogglers;
	public int[] screenWidths;
	int activeScreenResIndex;

	void Start () {
		activeScreenResIndex = PlayerPrefs.GetInt ("Screen res index");
		bool isFullscreen = (PlayerPrefs.GetInt ("fullscreen") == 1)?true:false;

		volumeSliders [0].value = AudioManager.instance.masterVolumePercent;
		volumeSliders [1].value = AudioManager.instance.musicVolumePercent;
		volumeSliders [2].value = AudioManager.instance.sfxVolumePercent;

		for (int i=0; i<resolutionTogglers.Length; i++) {
			resolutionTogglers [i].isOn = i == activeScreenResIndex;
		}

		SetFullscreen(isFullscreen);
	}

	public void Play() {
		SceneManager.LoadScene("Game");
	}

	public void Quit() {
		Application.Quit();
	}

	public void OptionsMenu() {
		mainMenuHolder.SetActive (false);
		optionsMenuHolder.SetActive (true);
	}

	public void MainMenu() {
		mainMenuHolder.SetActive (true);
		optionsMenuHolder.SetActive (false);
	}

	public void SetScreenResolution (int i) {
		if (resolutionTogglers[i].isOn) {
			activeScreenResIndex = i;
			float aspectRatio = 16 / 9f;
			Screen.SetResolution (screenWidths [i], (int)(screenWidths [i] / aspectRatio), false);
			PlayerPrefs.SetInt("Screen res index", activeScreenResIndex);
		}
	}

	public void SetFullscreen (bool isFullScreen) {
		for (int i = 0; i < resolutionTogglers.Length; i++) {
			resolutionTogglers[i].interactable = !isFullScreen;
		}

		if (isFullScreen) {
			Resolution[] allResolutions = Screen.resolutions;
			Resolution maxResolution = allResolutions [allResolutions.Length - 1];
			Screen.SetResolution (maxResolution.width, maxResolution.height, true);
		} else {

			SetScreenResolution(activeScreenResIndex);

		}
		PlayerPrefs.SetInt("fullscreen", ((isFullScreen) ? 1:0));
		PlayerPrefs.Save();
	}

	public void SetMasterVolume (float value) {
		AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Master);
	}
	public void SetMusicVolume (float value) {
		AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Music);

	}
	public void SetSfxVolume (float value) {
		AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Sfx);

	}
}
