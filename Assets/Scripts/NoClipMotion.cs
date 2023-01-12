using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoClipMotion : MonoBehaviour
{
    Player _player;
    Rigidbody2D _rb;

    [SerializeField] bool noClipEnabled;
    [SerializeField] float moveSpeed = 15;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
            ToggleClipMotion();

        MovePlayer();
    }

    private void ToggleClipMotion()
    {
        noClipEnabled = !noClipEnabled;
        if (noClipEnabled)
        {
            _player.enabled = false;
            _rb.isKinematic = true;
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = 0;
        }
        else
        {
            _player.enabled = true;
            _rb.isKinematic = false;
        }
    }

    private void MovePlayer()
    {
        Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movementVector *= moveSpeed * Time.unscaledDeltaTime;
        if (Input.GetKey(KeyCode.LeftShift))
            movementVector *= 2;

        transform.Translate(movementVector);
    }
}
