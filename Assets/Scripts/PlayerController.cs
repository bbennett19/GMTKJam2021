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
    [SerializeField]
    private LayerMask _levelExitLayerMask;
    [SerializeField]
    private float _levelExityDetectionDistance;

    public bool isSeeingPlayer;
    
    private MouseLook _mouseLook;
    private PlayerMovement _playerMovement;
    private CharacterController _characterController;

    private bool _playerActive;
    private bool _hiding = false;
    private Transform _hideyHoleTransform;
    private int frame = 0;

    public void Awake()
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
            if (!CheckHideyHole() && !CheckLevelExit())
            {
                ButtonPromptOverlayController.Instance.HidePromt();
            }
        }
    }

    private bool CheckHideyHole()
    {
        RaycastHit hit;
        bool canHide = Physics.Raycast(transform.position, transform.forward, out hit, _hideyHoleDetectionDistance, _hideyHoleLayerMask);

        if (!_hiding && canHide && Input.GetKeyDown(KeyCode.E))
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
            return true;
        }
        else if (_hiding)
        {
            Debug.Log("Should show leave prompt");
            ButtonPromptOverlayController.Instance.ShowPromt("Press E to Exit");
            return true;
        }

        return false;
    }

    private bool CheckLevelExit()
    {
        RaycastHit hit;
        bool canExit = Physics.Raycast(transform.position, transform.forward, out hit, _levelExityDetectionDistance, _levelExitLayerMask);

        if (canExit && LevelExitManager.Instance.CanPlayerExit(isSeeingPlayer) &&  Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Exit");
            LevelExitManager.Instance.PlayerExited();
        }
        else if (canExit && LevelExitManager.Instance.CanPlayerExit(isSeeingPlayer))
        {
            Debug.Log("Can Exit");
            ButtonPromptOverlayController.Instance.ShowPromt("Press E to Exit Level");
            return true;
        }

        return false;
    }
}
