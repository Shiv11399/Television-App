using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;
using System;

public enum SliderState
{
    Free = 0,
    Selected = 1,
}

public class Television : MonoBehaviour
{
    public VideoClip[] videoClips;
    public VideoPlayer player;
    public CustomSlider slider;
    private SliderState currentSliderState = SliderState.Free;
    public GameObject selectionScreen;

    public Sprite playSprite;
    public Sprite pauseSprite;

    public Button playButton;

    void Awake()
    {
        player.Pause();
    }

    public void SetFirstVideo()
    {
        player.clip = videoClips[0];
        StartVideo();
    }
    public void SetSecondVideo()
    {
        player.clip = videoClips[1];
        StartVideo();
    }
    public void SetThirdVideo()
    {
        player.clip = videoClips[2];
        StartVideo();
    }
    void StartVideo()
    {
        slider.maxValue = (float)player.clip.length;
        selectionScreen.SetActive(false);
        player.time = 0;
        player.Play();
        SetButtonSprite(true);
    }

    void Update()
    {
        if (slider.IsFocused)
        {
            currentSliderState = SliderState.Selected;
        }
        else
        {
            currentSliderState = SliderState.Free;
            slider.value = (float)player.time;
        }
    }
    public void BackToGallery()
    {
        player.Pause();
        selectionScreen.SetActive(true);
    }
    public void PlayButtonPressed()
    {
        if (player.isPaused)
        {
            player.Play();
            SetButtonSprite(true);
        }
        else
        {
            player.Pause();
            SetButtonSprite(false);
        }
    }

    private void SetButtonSprite(bool state)
    {
        playButton.image.sprite = state ? pauseSprite : playSprite;
    }

    public void OnSliderValueChanged(float value)
    {
        // Seek to the specified time based on the slider value
        if (currentSliderState == SliderState.Free)
        {
            slider.value = (float)player.time;
        }
        else
        {
            player.time = value;
        }
    }
    // Function to handle forwarding the video
    public void Forward(float seconds)
    {
        player.time += seconds;
    }

    // Function to handle rewinding the video
    public void Rewind(float seconds)
    {
        player.time -= seconds;
    }

    // Function to seek to a specific point in the video
    public void Seek(float time)
    {
        player.time = time;
    }

}
