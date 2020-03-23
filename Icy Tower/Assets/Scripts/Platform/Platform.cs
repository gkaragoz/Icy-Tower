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
        SetScale();
        SetPosition();
    }

    private void SetScale() {
        transform.localScale = new Vector3(Random.Range(_platformStats.GetMinScale(), _platformStats.GetMaxScale()), _platformStats.GetThickness(), _platformStats.GetPrefab().transform.localScale.z);
    }

    private void SetPosition() {
        Vector3 _randomPos = new Vector3(Random.Range(GameManager.instance.LeftMapSpawnTransform.position.x, GameManager.instance.RightMapSpawnTransform.position.x),
                                         SpawnManager.instance.LastSpawnedPlatformPos += _platformStats.GetDistanceBetweenPlatforms(),                                                                                                //y
                                         0f);

        transform.position = _randomPos;
    }

    public void OnObjectReused() {
        gameObject.SetActive(true);
    }
}
