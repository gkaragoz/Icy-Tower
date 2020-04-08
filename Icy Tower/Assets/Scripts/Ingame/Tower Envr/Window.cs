using UnityEngine;

public class Window : MonoBehaviour, IPooledObject {
    public void OnObjectReused() {
        gameObject.SetActive(true);
    }
}
