using UnityEngine;
[RequireComponent(typeof(PlatformStats))]

public class Platform : MonoBehaviour, IPooledObject {

    [SerializeField]
    [Utils.ReadOnly]
    private PlatformStats _platformStats;

    private void Awake() {
        _platformStats = GetComponent<PlatformStats>();
    }

    private void SetScale() {
        transform.localScale = new Vector3(Random.Range(_platformStats.GetMinScale(), _platformStats.GetMaxScale()), _platformStats.GetThickness(), _platformStats.GetPrefab().transform.localScale.z);
    }

    private void SetPosition() {
        float x = WorldSettings.instance.GetRandomPosition().x;
        float y = SpawnManager.instance.LastSpawnedPlatformPos += _platformStats.GetDistanceBetweenPlatforms();
        float z = 0;
        Vector3 _randomPos = new Vector3(x, y, z);

        transform.position = _randomPos;
    }

    public void OnObjectReused() {
        gameObject.SetActive(true);
        SetPosition();
        SetScale();
    }
}
