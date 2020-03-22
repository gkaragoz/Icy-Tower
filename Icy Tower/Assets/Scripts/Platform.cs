using UnityEngine;

public class Platform : MonoBehaviour, IPooledObject {

    [SerializeField]
    private int _maxPlatformScale = 0;
    [SerializeField]
    private int _minPlatformScale = 0;



    public void OnObjectReused() {
        gameObject.SetActive(true);
    }

    public int GetMaxPlatformScale() {
        return _maxPlatformScale;
    }

    public int GetMinPlatformScale() {
        return _minPlatformScale;
    }
}
