using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverCanvas : MonoBehaviour
{
    [SerializeField]
    private RawImage _image;
    [SerializeField]
    private TMP_Text _text;
    [SerializeField]
    private Button _button;
    [SerializeField]
    private float _fadeTime;

    private Color _white = new Color(1f, 1f, 1f, 0f);
    private Color _black = new Color(0f, 0f, 0f, 0f);

    private static GameOverCanvas _instance;

    public static GameOverCanvas Instance { get { return _instance; } }

    private void Awake()
    {
        _instance = this;    
    }

    public void TriggerGameOver(PlayerController playerController)
    {
        StartCoroutine(GameOver(playerController));
    }

    IEnumerator GameOver(PlayerController playerController)
    {
        bool currentPlayerSeeing = PlayerManager.Instance.GetCurrentPlayer().GetComponent<PlayerController>().isSeeingPlayer;

        if (currentPlayerSeeing)
        {
            _image.color = _black;
        } else
        {
            _image.color = _white;
        }

        float elapsedTime = 0f;
        float alpha = 0;

        while (alpha < 1f) {
            elapsedTime += Time.deltaTime;
            alpha = Mathf.Lerp(0f, 1f, elapsedTime / _fadeTime);
            _image.color = new Color(_image.color.r, _image.color.b, _image.color.g, alpha);
            yield return null;
        }

        yield return null;

        // display text and button
        _text.color = currentPlayerSeeing ? Color.white : Color.black;
        _text.gameObject.SetActive(true);
        _button.gameObject.SetActive(true);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
