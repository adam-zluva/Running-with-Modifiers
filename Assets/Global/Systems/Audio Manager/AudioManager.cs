using System.Collections.Generic;
using UnityEngine;
using EventChannels;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioRequestEventChannel audioChannel;

    private List<AudioSource> audioSources;

    private void Awake()
    {
        audioSources = new List<AudioSource>();
    }

    private void OnAudioRequested(Sound sound)
    {
        AudioSource source = GetFreeSource();

        source.clip = sound.clip;
        source.volume = sound.volume;
        source.pitch = sound.pitch;

        source.Play();
    }

    private AudioSource GetNewSource()
    {
        var newSourceObj = new GameObject("Audio Source");
        newSourceObj.transform.SetParent(transform);

        return newSourceObj.AddComponent<AudioSource>();
    }

    private AudioSource GetFreeSource()
    {
        foreach (var source in audioSources)
        {
            if (!source.isPlaying) return source;
        }

        return GetNewSource();
    }

    private void OnEnable()
    {
        audioChannel.onEventRaised += OnAudioRequested;
    }

    private void OnDisable()
    {
        audioChannel.onEventRaised -= OnAudioRequested;
    }
}
