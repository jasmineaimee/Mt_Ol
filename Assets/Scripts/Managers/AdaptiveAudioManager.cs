using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AdaptiveAudioManager : Singleton<AdaptiveAudioManager>
{
    [Header("A D A P T I V E  A U D I O  M A N A G E R")]
    [Header("Set In Inspector")]
    public AudioMixerSnapshot[] snapshotLevels; // the audio mix snapshots
    [Header("Set Dynamically")]
    public int currentAdaptiveLevel; // what level area we're in
    public float transitionTime = 1; // transition between audio mixes

	public void AdjustAudioLevel(int level)
    {
        // new level area, transition music
        currentAdaptiveLevel = level;
        snapshotLevels[currentAdaptiveLevel-1].TransitionTo(transitionTime);
    }
}
