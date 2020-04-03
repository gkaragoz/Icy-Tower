using UnityEngine;

public class PlatformSaver : MonoBehaviour {

    [Utils.ReadOnly]
    [SerializeField]
    private PlatformSaverStats _platformSaverStats = null;
    [Utils.ReadOnly]
    [SerializeField]
    private int _platformCountToMaximize;
    [Utils.ReadOnly]
    [SerializeField]
    private Platform[] _platforms;
    [Utils.ReadOnly]
    [SerializeField]
    private GameState _gameState;



    private void Start() {
        _platformSaverStats = GetComponent<PlatformSaverStats>();
        _platformCountToMaximize = _platformSaverStats.GetPlatformCount();

    }

    private void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.M))
            MaximizePlatformScale();

        _gameState = GameManager.instance.GameStateEnum;
        if (_gameState == GameState.Gameplay)
            GetPlatforms();
    }

    private void GetPlatforms() {
        _platforms = new Platform[ObjectPooler.instance.GetGameObjectsOnPool("Platform").Length];
        for (int i = 0; i < _platforms.Length; i++) {
            _platforms[i] = ObjectPooler.instance.GetGameObjectsOnPool("Platform")[i].GetComponent<Platform>();
        }
    }

    private void MaximizePlatformScale() {
        for (int i = 0; i < _platformCountToMaximize; i++) {
            _platforms[i].gameObject.transform.localScale = new Vector3(10, _platforms[i].gameObject.transform.localScale.y, _platforms[i].gameObject.transform.localScale.y);
            _platforms[i].gameObject.transform.position = new Vector3(0, _platforms[i].gameObject.transform.position.y, _platforms[i].gameObject.transform.position.z);
        }
    }

}
