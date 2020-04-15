using UnityEngine;


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

        foreach (string collectable in (string[])System.Enum.GetNames(typeof(Collectables))) {
            if(other.tag == collectable) {
                other.gameObject.SetActive(false);
            }
        }
    }
}
