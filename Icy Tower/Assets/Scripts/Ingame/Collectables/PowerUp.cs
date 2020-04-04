using UnityEngine;

public class PowerUp : MonoBehaviour, IPooledObject {
    public void OnObjectReused() {
        gameObject.SetActive(true);
    }
}
