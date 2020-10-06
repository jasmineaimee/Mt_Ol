using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AdaptiveAudioManager : Singleton<AdaptiveAudioManager>
{

    public int currentAdaptiveLevel; // what level area we're in
    public AudioMixerSnapshot[] snapshotLevels; // the audio mix snapshots
    public float transitionTime = 1; // transition between audio mixes

	public void AdjustAudioLevel(int level)
    {
        // new level area, transition music
        currentAdaptiveLevel = level;
        snapshotLevels[currentAdaptiveLevel-1].TransitionTo(transitionTime);
    }
}
