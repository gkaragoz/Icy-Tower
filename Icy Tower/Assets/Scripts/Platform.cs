using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    [SerializeField]
    private float _jumpForce = 5f;
    [SerializeField]
    private float _comboJumoForce = 10f;
    [SerializeField]
    private float _comboTrashHold = 0f;


    void OnCollisionEnter2D(Collision2D collision) {

        if (collision.relativeVelocity.y <= 0f) {
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();

            if (rb != null) {
                Vector2 velocity = rb.velocity;
                velocity.y = Mathf.Abs(velocity.x) > _comboTrashHold ? _comboJumoForce : _jumpForce;
                rb.velocity = velocity;
            }
        }
    }

}
