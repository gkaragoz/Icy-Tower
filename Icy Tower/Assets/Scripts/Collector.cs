using UnityEngine;
using UnityEngine.SceneManagement;


public class Collector : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Platform") {
            other.gameObject.SetActive(false);
            SpawnManager.instance.SpawnPlatform();
        } else if (other.tag == "Player") {
            SceneManager.LoadScene("Main");
        }else if(other.tag == "Wall") {
            other.gameObject.SetActive(false);
            SpawnManager.instance.SpawnWall();
        }
    }
}
