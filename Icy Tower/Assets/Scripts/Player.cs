using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private float _movementSpeed;

    Rigidbody2D rb;

    private float _movement = 0f;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {

        _movement = (Input.GetAxis("Horizontal") * _movementSpeed);
    }

    void FixedUpdate() {
        Vector2 velocity = rb.velocity;
        velocity.x = _movement;
        rb.velocity = velocity;
    }
}
