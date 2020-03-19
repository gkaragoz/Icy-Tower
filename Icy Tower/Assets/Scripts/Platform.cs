using UnityEngine;

public class Platform : MonoBehaviour, IPooledObject {
    public void OnObjectReused() {
        gameObject.SetActive(true);
    }
}
