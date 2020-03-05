using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private float _movementSpeed = 0f;
    [SerializeField]
    private Joystick _joystick;
    [SerializeField]
    private Transform _gfx;

    public Animator anim;

    private Rigidbody2D _rb;

    private float _movement = 0f;
    private Vector3 _scale;
    private bool _isPlayerMoved = false;
    private bool _isRunning = false;
    private bool _hasJumped = false;

    void Start() {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update() {

        // _movement = (Input.GetAxis("Horizontal") * _movementSpeed);
        _movement = _joystick.Horizontal * _movementSpeed;
    }

    void FixedUpdate() {
        Vector2 velocity = _rb.velocity;
        velocity.x = _movement;
        if (velocity.x != 0) {
            _isPlayerMoved = true;
            _isRunning = true;
        } else {
            _isRunning = false;
        }
        SetDirectionOfCharacter(velocity);
        PlayRunAnimation();
        _rb.velocity = velocity;
    }

    private void SetDirectionOfCharacter(Vector2 velocity) {
        if (_isPlayerMoved) {
            if (velocity.x > 0)
                _scale = Vector3.one;
            if (velocity.x < 0)
                _scale = new Vector3(-1, _gfx.localScale.y, _gfx.localScale.z);
            _gfx.localScale = _scale;
            _isPlayerMoved = false;
        }

    }

    private void PlayRunAnimation() {
        if (_isRunning)
            anim.SetBool("isRunning", _isRunning);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Platform") {
            _hasJumped = true;
            anim.SetBool("hasJumped", _hasJumped);
        }
    }
}
