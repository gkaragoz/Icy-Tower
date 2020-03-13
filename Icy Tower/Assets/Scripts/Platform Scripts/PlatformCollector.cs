using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Platform") {
            collision.gameObject.SetActive(false);
            PlatformGenerator.instance.SpawnPlatform();
        }
        if(collision.gameObject.tag == "Player") {
            GameManager.instance.GameOver();
        }
    }
}
