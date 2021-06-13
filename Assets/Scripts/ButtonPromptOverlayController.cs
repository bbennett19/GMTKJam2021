using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonPromptOverlayController : MonoBehaviour
{
    [SerializeField]
    private Canvas _canvas;
    [SerializeField]
    private TMP_Text _text;

    private static ButtonPromptOverlayController _instance;

    public static ButtonPromptOverlayController Instance { get { return _instance; } }

    private void Awake()
    {
        _instance = this;
    }

    public void ShowPromt(string text)
    {
        _text.text = text;
        _canvas.gameObject.SetActive(true);
    }

    public void HidePromt()
    {
        _canvas.gameObject.SetActive(false);
    }
}
