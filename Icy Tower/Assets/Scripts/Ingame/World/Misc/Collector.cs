using System;
using UnityEngine;


public class Collector : MonoBehaviour {

    [SerializeField]
    private bool _isCollectorActive = false;

    private void Start() {
        GameManager.instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState previousState, GameState currentState) {
        if (currentState == GameState.GameOver) {
            _isCollectorActive = false;
        }
        if (currentState == GameState.Gameplay) {
            _isCollectorActive = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (_isCollectorActive == false) {
            return;
        }

        if (other.tag == "Platform") {
            Debug.Log("Collect platform");
            other.gameObject.SetActive(false);
            PlatformManager.instance.SpawnPlatform().gameObject.SetActive(true);
        }
        
        else if(other.tag == "WallParent") {
            Debug.Log("Collect wall");
            other.gameObject.SetActive(false);
            SpawnManager.instance.SpawnWall();
        }else if(other.tag == "Gold") {
            other.gameObject.SetActive(false);
        }

        foreach (string collectable in (string[])System.Enum.GetNames(typeof(Collectables))) {
            if(other.tag == collectable) {
                other.gameObject.SetActive(false);
            }
        }
    }
}
