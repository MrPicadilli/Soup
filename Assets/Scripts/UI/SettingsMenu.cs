using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
 private Resolution[] resolutions;
 public TMPro.TMP_Dropdown resolutionDropdown;
 public AudioMixer audioMixer;
 public Slider soundSlider;
 private float startVolume;
 public TMPro.TMP_Dropdown qualityDropdown;
 private void Start()
 {
  audioMixer.GetFloat("Volume", out startVolume);
  soundSlider.value = startVolume;
/*  resolutions = Screen.resolutions;
  resolutionDropdown.ClearOptions();
  List<String> options = new List<string>();
  int index = 0;
  qualityDropdown.SetValueWithoutNotify(1);
  QualitySettings.SetQualityLevel(1);
  for (int i = 0; i < resolutions.Length; i++)
  {
   index += 1;
   string option = resolutions[i].width + " x " + resolutions[i].height;
   options.Add(option);
   if (resolutions[i].height == Screen.currentResolution.height && resolutions[i].width == Screen.currentResolution.width)
   {
    index = i;
   }
  }
  resolutionDropdown.AddOptions(options);
  resolutionDropdown.value = index;
  resolutionDropdown.RefreshShownValue();*/
 }

 
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

 public void SetResolution(int resolutionIndex)
 {
  Resolution resolution = resolutions[resolutionIndex];
  Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
  
 }
 
}
