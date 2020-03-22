using UnityEngine;
[RequireComponent(typeof(PlatformStats))]

public class Platform : MonoBehaviour, IPooledObject {

    [SerializeField]
    [Utils.ReadOnly]
    private PlatformStats _platformStats;

    private void Awake() {
        _platformStats = GetComponent<PlatformStats>();
    }

    private void Start() {
        transform.localScale = new Vector3(Random.Range(_platformStats.GetMinScale(),_platformStats.GetMaxScale()),_platformStats.GetThickness() , _platformStats.GetPrefab().transform.localScale.z);
    }

    public void OnObjectReused() {
        gameObject.SetActive(true);
    }

    public int GetMaxPlatformScale() {
        return _platformStats.GetMaxScale();
    }

    public int GetMinPlatformScale() {
        return _platformStats.GetMinScale();
    }
}
