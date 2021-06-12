using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _enabledWhenActive;
    [SerializeField]
    private List<GameObject> _disableWhenActive;
    
    private MouseLook _mouseLook;
    private PlayerMovement _playerMovement;

    public void Start()
    {
        _mouseLook = GetComponent<MouseLook>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    public void SetPlayerActive(bool active)
    {
        foreach (GameObject gameObject in _enabledWhenActive)
        {
            gameObject.SetActive(active);
        }

        foreach (GameObject gameObject in _disableWhenActive)
        {
            gameObject.SetActive(!active);
        }

        _mouseLook.enabled = active;
        _playerMovement.enabled = active;
    }
}
