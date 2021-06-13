using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveVelocity;
    [SerializeField]
    private CharacterController _characterController;
    [SerializeField]
    private AudioClipRoundRobin _clipPlayer;

    private float gravity = 100f;

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        // keep the player on the ground
        float y = _characterController.isGrounded ? 0f : gravity;

        Vector3 velocity = transform.right * x + transform.forward * z + transform.up * -y;
        
        _characterController.Move(velocity * _moveVelocity * Time.deltaTime);

        _clipPlayer.SetPlaying(new Vector2(velocity.x, velocity.z).magnitude > 0f);
    }

    private void OnDisable()
    {
        _clipPlayer.SetPlaying(false);
    }
}
