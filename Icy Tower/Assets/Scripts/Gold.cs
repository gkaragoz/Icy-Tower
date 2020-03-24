using UnityEngine;

public class Gold : MonoBehaviour, IPooledObject {
    [System.Obsolete]
    public void OnObjectReused() {
        gameObject.SetActiveRecursively(true);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "FullCollider") {
            this.gameObject.SetActive(false);
        }
    }

    //TODO : Play Gold pickup Sound
}
