using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideyHole : MonoBehaviour
{
    [SerializeField]
    private Transform _hidingPositionTransform;

    public Transform GetHidingTransform()
    {
        return _hidingPositionTransform;
    }
}
