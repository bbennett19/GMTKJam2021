using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveVelocity;
    [SerializeField]
    private CharacterController _characterController;

    private float gravity = 100f;

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        // keep the player on the ground
        float y = _characterController.isGrounded ? 0f : gravity;

        Vector3 velocity = transform.right * x + transform.forward * z + transform.up * -y;
        Debug.Log(velocity);
        _characterController.Move(velocity * _moveVelocity * Time.deltaTime);
    }
}
