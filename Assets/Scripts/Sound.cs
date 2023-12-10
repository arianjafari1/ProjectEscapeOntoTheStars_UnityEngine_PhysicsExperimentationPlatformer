using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable] // to make it visible in the Editor
public class Sound //custom class that doesn't inherit from Monobehaviour to store properties for the audiomanager
{
    public string nameSound; //name for the property sound in the editor

    public AudioClip clipSound; //reference to AudioPlayuer from UNity


    [Range(0f, 1f)] // to add a slider to those var in Unity Editor, we need to ad min and max
    public float volumeSound; //volume of sound
    [Range(0f, 1f)]
    public float pitchSound; //pitch of sound

    public bool loopSound; //option for looping the audio

    [HideInInspector] //hide it from the Unity Editor
    public AudioSource sourceSound; //for the play method, where the audio source is stored from Audio



}
