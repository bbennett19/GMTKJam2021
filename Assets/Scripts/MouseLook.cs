using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private float _mouseSensitivity = 100f;
    [SerializeField]
    private Transform _playerBody;
    [SerializeField]
    private Transform _camera;
    [SerializeField]
    private bool _yEnabled = true;
    [SerializeField]
    private float _xRestrictedAngle = 45f;

    private float _yRotation = 0f;
    private float _restrictedStartOrientation;
    private bool _restrictXRotation = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;

        // This doesn't work, putting it off for now
        if (_restrictXRotation)
        {
            Debug.Log(transform.rotation.eulerAngles.x + mouseX);
            Debug.Log((_restrictedStartOrientation + _xRestrictedAngle) + ":" + (_restrictedStartOrientation - _xRestrictedAngle));
            if (transform.rotation.eulerAngles.x + mouseX > _restrictedStartOrientation + _xRestrictedAngle || transform.rotation.eulerAngles.x + mouseX < _restrictedStartOrientation - _xRestrictedAngle)
            {
                mouseX = 0f;
            }
        }

        _playerBody.Rotate(Vector3.up * mouseX);

        if (_yEnabled)
        {
            float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

            _yRotation -= mouseY;
            _yRotation = Mathf.Clamp(_yRotation, -90f, 90f);

            _camera.transform.localRotation = Quaternion.Euler(_yRotation, 0f, 0f);
        }
    }

    public void SetXRestricted(bool restricted)
    {
        _restrictXRotation = restricted;
        _restrictedStartOrientation = _playerBody.rotation.eulerAngles.x;
    }
}
