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
    private float _playRate = 0.25f;
    private float _elapsed = 100f;

    // Update is called once per frame
    void Update()
    {
        _elapsed += Time.deltaTime;

        if (_playing && _elapsed >= _playRate)
        {
            _elapsed = 0f;
            _audioSource.PlayOneShot(_clips[_clipIndex]);
            _clipIndex = (_clipIndex + 1) % _clips.Count;
        }
    }

    public void SetPlaying(bool playing)
    {
        _playing = playing;
    }
}
