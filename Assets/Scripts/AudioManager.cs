using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour //custom AudioManager to make expansion of audio elements of the game is to undertake
{
    [SerializeField] private Sound[] sounds;
    public static AudioManager audioManagerInstance; //static reference to the current audio manager that we have on our scene

    // Start is called before the first frame update
    private void Awake() //similar to start method but called right before it
    {
        if (audioManagerInstance == null) // if we don't have an audio manager in our scene
        {
            audioManagerInstance = this; // then instance equals to this object
        } else
        {
            Destroy(gameObject); // destroy the other audio managers to avoid duplicates
            return; //just to be sure that no more coded will be executed after destroying the object
        }

        DontDestroyOnLoad(gameObject); // make sure that the AudioManager object that is now a prefab persists between scenes



        foreach (Sound sound in sounds) //looping through a list of sounds and add an audio source for each
        {
            //add the audio source component and store in a var, so that when we want to play a sound, we can call the play method on the source
            sound.sourceSound = gameObject.AddComponent<AudioSource>();
            //with these any modifications we make from the Unity Editor will actually take effect
            sound.sourceSound.clip = sound.clipSound; //clip of our audio source
            sound.sourceSound.volume = sound.volumeSound;
            sound.sourceSound.pitch = sound.pitchSound;
            sound.sourceSound.loop = sound.loopSound; //assigns the loop option from the audio manager to the loopSound from Sound class, same for the other ones
        }


    }

    private void Start() //we can use start option to play a background music and have it continously loop by ticking that in the editor
    {
        //if you want to play a song globally use our playSound mehthod (eg: playSound("nameOfTheSound");)
        playSound("themeSong");
    }


    public void playSound(string name) //method to play sound
    {
        //loop through all the audio and find the one with the correct name
        Sound sound = Array.Find(sounds, sound => sound.nameSound == name); //find the sound in sounds where sound name is equal to name, and store it to var sound

        if (sound == null) //check if the sound is null, if the name exists to begin with, maybe if you spell it wrong for example
        {
            Debug.LogWarning("Audio sound " + name + " could not be found"); //warning message in case sound couldn't be found
            return; //returning nothing
        }

        sound.sourceSound.Play();
    }

}
