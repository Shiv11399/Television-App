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
/// <summary>
/// A basic video player script to control intractions with the virtual TV.
/// </summary>
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

    public Material onMaterial;
    public Material offMaterial;
    public MeshRenderer lightMesh;
    public GameObject tvCanvas;
    public Text onAndOffText;

    void Awake()
    {

        player.Pause();
        lightMesh.material = offMaterial;
        TurnOff();
    }

    public void ToggleSwitch(bool state)
    {
        if (state) TurnOn();
        else TurnOff();
    }
    public void TurnOn()
    {
        tvCanvas.SetActive(true);
        player.enabled = true;
        lightMesh.material = onMaterial;
        onAndOffText.text = "ON";
    }
    public void TurnOff()
    {
        BackToGallery();
        tvCanvas.SetActive(false);
        player.enabled = false;
        lightMesh.material = offMaterial;
        onAndOffText.text = "OFF";
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
        CheckInputs();
    }

    private void CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (tvCanvas.activeSelf)
            {
                TurnOff();
            }
            else
            {
                TurnOn();
            }
        }
    }

    public void BackToGallery()
    {
        player.Pause();
        selectionScreen.SetActive(true);
        player.time = 0;
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
/// <summary>
/// Set the correct pause or play sprite on the button.
/// </summary>
/// <param name="state"></param>
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
