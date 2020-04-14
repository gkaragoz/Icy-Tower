using UnityEngine;

public class PowerUp : MonoBehaviour, IPooledObject {
    public void OnObjectReused() {
        SetPosition();
        gameObject.SetActive(true);
    }

    private void SetPosition() {
        
    }
}
