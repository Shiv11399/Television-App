using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// this a data container to store the data related to the video clip.
/// </summary>
[CreateAssetMenu(fileName = "New Clip Data", menuName = "Create Clip Data")]
public class VideoData : ScriptableObject
{
    public string title;
    public VideoClip clip;
    public float defaultVolume = 1f;

}
