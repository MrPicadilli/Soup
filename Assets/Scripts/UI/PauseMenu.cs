using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
 private Resolution[] resolutions;
 public static bool GameIsPaused = false;
 public GameObject PauseUI;
 public Slider soundSlider;
 private float startVolume;
 public TMPro.TMP_Dropdown qualityDropdown;
 private void Start()
 {
  
  audioMixer.GetFloat("Volume", out startVolume);
  soundSlider.value = startVolume;
 
 }

 private void Update()
 {
  if (Input.GetKeyDown(KeyCode.Escape))
  {
   if (GameIsPaused)
   {
    Resume();
   }
   else
   {
    Pause();
   }
  }
 }

 public void Resume()
 {
  PauseUI.SetActive(false);
  Time.timeScale = 1f;
  GameIsPaused = false;
 }

 void Pause()
 {
  PauseUI.SetActive(true);
  Time.timeScale = 0f;
  GameIsPaused = true;
 }

 public AudioMixer audioMixer;
 public void SetVolume(float volume)
 {
  audioMixer.SetFloat("Volume", volume);
 }

 public void SetQuality(int qualityIndex)
 {
  QualitySettings.SetQualityLevel(qualityIndex);
 }

 public void SetFullscreen(bool isFullscreen)
 {
  Screen.fullScreen = isFullscreen;
 }

 public void GoToMenu()
 {
  Time.timeScale = 1f;
  SceneManager.LoadScene("Menu");
 }
 
}
