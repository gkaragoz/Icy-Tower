using UnityEngine;

public class Collector : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Platform") {
            other.gameObject.SetActive(false);
            SpawnManager.instance.SpawnPlatform();
        }
    }
}
