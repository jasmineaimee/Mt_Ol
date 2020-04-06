using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [Header("S O U N D  M A N A G E R")]
    [Header("Set In Inspector")]
    public AudioClip collectableClip;
    public AudioClip incorrectClip;
    public AudioClip correctClip;
    public AudioClip teleportClip;
    public AudioMixer masterMixer;
    
    [Header("Set Dynamically")]
    public static SoundManager Instance;

    // Private Vars
    private AudioSource soundEffectAudio;
    private float soundLevel = -80.0f;

    void Start()
    {
        Instance = this;
        AudioSource[] sources = GetComponents<AudioSource>();
        foreach(AudioSource source in sources)
        {
            if(source.clip == null)
            {
                soundEffectAudio = source;
            }
        }
    }

    public void PlayOneShot(AudioClip clip)
    {
        soundEffectAudio.PlayOneShot(clip);
    }

    public void MuteGame()
    {
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
        soundEffectAudio.mute = !soundEffectAudio.mute;
    }
}
