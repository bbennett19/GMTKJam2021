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
    private int _clipIndex;

    // Update is called once per frame
    void Update()
    {
        if (!_audioSource.isPlaying && _playing)
        {

        }
    }
}
