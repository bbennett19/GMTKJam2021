using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _enabledWhenActive;
    [SerializeField]
    private List<GameObject> _disableWhenActive;
    [SerializeField]
    private LayerMask _hideyHoleLayerMask;
    [SerializeField]
    private float _hideyHoleDetectionDistance;
    
    private MouseLook _mouseLook;
    private PlayerMovement _playerMovement;
    private CharacterController _characterController;

    private bool _playerActive;
    private bool _hiding = false;
    private Transform _hideyHoleTransform;
    private int frame = 0;

    public void Start()
    {
        _mouseLook = GetComponent<MouseLook>();
        _playerMovement = GetComponent<PlayerMovement>();
        _characterController = GetComponent<CharacterController>();
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
        _playerActive = active;
    }

    private void LateUpdate()
    {
        if (_playerActive)
        {
            RaycastHit hit;
            bool canHide = Physics.Raycast(transform.position, transform.forward, out hit, _hideyHoleDetectionDistance, _hideyHoleLayerMask);

            Debug.Log(canHide);
            if (!_hiding && canHide && Input.GetKey(KeyCode.E))
            {
                Debug.Log("Hide");
                _characterController.enabled = false;
                _hiding = true;
                _hideyHoleTransform = hit.transform.gameObject.GetComponent<HideyHole>().GetHidingTransform();
                transform.position = _hideyHoleTransform.position;
                transform.rotation = _hideyHoleTransform.rotation;
                // _mouseLook.SetXRestricted(true);
            }
            else if (_hiding && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Leave hiding");
                _hiding = false;
                transform.position = transform.position + (_hideyHoleTransform.forward * 3f);
                _characterController.enabled = true;
                //_mouseLook.SetXRestricted(false);
            }

            // very inefficient, should only do these when the hiding/canhide state changes
            if (!_hiding && canHide)
            {
                Debug.Log("Should show hide prompt");
                ButtonPromptOverlayController.Instance.ShowPromt("Press E to Hide");
            }
            else if (_hiding)
            {
                Debug.Log("Should show leave prompt");
                ButtonPromptOverlayController.Instance.ShowPromt("Press E to Exit");
            }
            else if (!_hiding && !canHide)
            {
                Debug.Log("Should clear prompt");
                ButtonPromptOverlayController.Instance.HidePromt();
            }
        }
    }
}
