using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardObject : MonoBehaviour
{
    [SerializeField]
    GameObject _front;
    [SerializeField]
    GameObject _back;
    [SerializeField]
    Transform _parentTransform;

    private Transform _cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        _cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, _cameraTransform.rotation.eulerAngles.y, 0f);

        if (_front != null && _back != null)
        {
            Vector3 direction = (_cameraTransform.position - _parentTransform.position).normalized;
            float value = Vector3.Dot(direction, _parentTransform.forward);

            Debug.Log(value);
            if (value > 0)
            {
                _back.SetActive(false);
                _front.SetActive(true);
            } else
            {
                _back.SetActive(true);
                _front.SetActive(false);
            }
        }
    }
}
