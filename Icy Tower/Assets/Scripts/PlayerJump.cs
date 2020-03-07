using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

    public float height;
    public float flyTime;
    public float playerY;
    private bool isJumping;
    public float gravityScale;
    public float graphX;

    IEnumerator Jump() {
        while (isJumping) {
            playerY = height * ((flyTime * (graphX) - Mathf.Pow(graphX, 2)) / (flyTime * flyTime));
            graphX += gravityScale;
            transform.position = new Vector3(transform.position.x,  playerY, transform.position.z);
            ResetValues();
            yield return null;
        }
    }

    private void ResetValues() {
        if (graphX >= flyTime) {
            isJumping = false;
            graphX = 0;
            playerY = 0;
            transform.position = new Vector3(transform.position.x, playerY, transform.position.z);
        }
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            isJumping = true;
            StartCoroutine(Jump());
        }
    }
}
