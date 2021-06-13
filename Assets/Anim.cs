using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Anim : MonoBehaviour
{
	public Sprite[] sprites;
	public int _frameRate = 6;
	public bool loop = true;

	private int _frameIndex = 0;
	private Image image;
	private float _timePerFrame;
	private float _time = 0f;

	void Awake()
	{
		image = GetComponent<Image>();
		_timePerFrame = 1f / _frameRate;
	}

	void Update()
	{
		_time += Time.deltaTime;

		if (_time >= _timePerFrame)
		{
			_time = 0f;
			_frameIndex = (_frameIndex + 1) % (sprites.Length);
			image.sprite = sprites[_frameIndex];
		}
	}
}
