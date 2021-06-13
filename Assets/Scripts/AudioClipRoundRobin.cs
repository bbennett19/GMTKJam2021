using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipRoundRobin : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> _clips;
    [SerializeField]
    private AudioSource _audioSource;

    private bool _playing;
    private int _clipIndex = 0;

    // Update is called once per frame
    void Update()
    {
        if (!_audioSource.isPlaying && _playing)
        {
            _audioSource.PlayOneShot(_clips[_clipIndex]);
            _clipIndex = (_clipIndex + 1) % _clips.Count;
        }
    }

    public void SetPlaying(bool playing)
    {
        _playing = playing;
    }
}
