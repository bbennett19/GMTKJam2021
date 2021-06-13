using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialOffsetAnimator : MonoBehaviour
{
    [SerializeField]
    private int _numberOfFrames;
    [SerializeField]
    private int _frameRate;

    private Renderer _renderer;
    private int _frameIndex = 0;
    private float _timePerFrame;
    private float _time;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _timePerFrame = 1f / _frameRate;
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;

        if (_time >= _timePerFrame)
        {
            _time = 0f;
            _frameIndex = (_frameIndex + 1) % (_numberOfFrames+1);
            Debug.Log("FrameIndex: " + _frameIndex);
            Debug.Log((float) _frameIndex / _numberOfFrames);
            _renderer.material.SetTextureOffset("_BaseMap", new Vector2((float)_frameIndex / _numberOfFrames, 0f));
        }
    }
}
