using UnityEngine;
using UnityEngine.SceneManagement;


public class Collector : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Platform") {
            other.gameObject.SetActive(false);
            PlatformManager.instance.SpawnPlatform().gameObject.SetActive(true);
        }
        
        else if(other.tag == "WallParent") {
            other.gameObject.SetActive(false);
            SpawnManager.instance.SpawnWall();
        }
    }
}
