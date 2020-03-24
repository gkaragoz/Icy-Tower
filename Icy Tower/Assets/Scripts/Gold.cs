using UnityEngine;

public class Gold : MonoBehaviour, IPooledObject {
    public void OnObjectReused() {
        gameObject.SetActive(true);
    }
}
