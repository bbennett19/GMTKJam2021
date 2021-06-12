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

    private float _yRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        _playerBody.Rotate(Vector3.up * mouseX);

        if (_yEnabled)
        {
            float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

            _yRotation -= mouseY;
            _yRotation = Mathf.Clamp(_yRotation, -90f, 90f);

            _camera.transform.localRotation = Quaternion.Euler(_yRotation, 0f, 0f);
        }
    }
}
