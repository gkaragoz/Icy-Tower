﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour , IPooledObject{

   
    [SerializeField]
    private float _jumpForce = 5f;
    [SerializeField]
    private float _comboJumpForce = 10f;
    [SerializeField]
    private float _comboTrashHold = 0f;
    

    void OnCollisionEnter2D(Collision2D collision) {

        if (collision.relativeVelocity.y <= 0f) {
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();

            if (rb != null) {
                Vector2 velocity = rb.velocity;
                velocity.y = Mathf.Abs(velocity.x) > _comboTrashHold ? _comboJumpForce : _jumpForce;
                rb.velocity = velocity;
            }
        }
    }
    public void OnObjectReused() {
        gameObject.SetActive(true);
    }


}