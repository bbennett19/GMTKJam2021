using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardObject : MonoBehaviour
{
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
    }
}
