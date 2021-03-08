using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; // only want one SoundManager
    [Header("Set In Inspector")]
    [Header("S O U N D  M A N A G E R")]
    //public AudioClip collectableClip;
    public AudioClip incorrectClip; // clip that plays when player doesn't solve riddle
    public AudioClip correctClip; // clip that plays when player solves riddle
    public AudioClip teleportClip; // clip that plays when teleporting player
    public AudioMixer masterMixer; // the sound mixer
    // [Header("Set Dynamically")]

    // Private Vars
    private AudioSource soundEffectAudio; // which sound effect channel
    private float soundLevel = -80.0f; // to mute or unmute sounds.

    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            // grab all audiosources on this gameObject if it doesn't already have a clip, that's for sound effects.
            AudioSource[] sources = GetComponents<AudioSource>();
            foreach(AudioSource source in sources)
            {
                if(source.clip == null)
                {
                    soundEffectAudio = source;
                }
            }
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void PlayOneShot(AudioClip clip)
    {
        // play the sound effect once
        soundEffectAudio.PlayOneShot(clip);
    }

    public void MuteGame()
    {
        // toggle mute.
        if(soundLevel == -80.0f)
        {
            soundLevel = -20.0f;
        }
        else
        {
            soundLevel = -80.0f;
        }
        masterMixer.SetFloat("masterSound", soundLevel);
    }

    public void MuteEffects()
    {
        // mute sound effects
        soundEffectAudio.mute = !soundEffectAudio.mute;
    }

    public void PlayRiddleSound(int soundToPlay)
    {
        // TODO: Play sound from array.
        Debug.Log("Playing sound number: " + soundToPlay);
    }
}
