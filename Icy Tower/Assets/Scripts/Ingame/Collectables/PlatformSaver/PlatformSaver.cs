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


    private void Start() {
        _platformSaverStats = GetComponent<PlatformSaverStats>();
        _platformCountToMaximize = _platformSaverStats.GetPlatformCount();
        GameManager.instance.OnGameStateChanged += GetPlatforms;

    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "PlatformSaver") {
            _platformCountToMaximize = _platformSaverStats.GetPlatformCount();
            MaximizePlatformScale();
        }
    }

    private void GetPlatforms(GameState state) {
        if (state == GameState.Gameplay) {
            _platforms = new Platform[ObjectPooler.instance.GetGameObjectsOnPool("Platform").Length];
            for (int i = 0; i < _platforms.Length; i++) {
                _platforms[i] = ObjectPooler.instance.GetGameObjectsOnPool("Platform")[i].GetComponent<Platform>();
            }
        }
    }

    private void MaximizePlatformScale() {
        for (int i = 0; i < _platforms.Length; i++) {
            if (IsPlatformOnScreen(_platforms[i])) {
                _platforms[i].gameObject.transform.localScale = new Vector3(10, _platforms[i].gameObject.transform.localScale.y, _platforms[i].gameObject.transform.localScale.y);
                _platforms[i].gameObject.transform.position = new Vector3(0, _platforms[i].gameObject.transform.position.y, _platforms[i].gameObject.transform.position.z);
                _platformCountToMaximize--;
                if (_platformCountToMaximize == 0)
                    break;
            }
        }
    }

    private bool IsPlatformOnScreen(Platform platform) {
        float bottomOfScreen = Camera.main.transform.position.y - 8;
        float topOfScreen = Camera.main.transform.position.y + 8f;

        if (platform.gameObject.transform.position.y > bottomOfScreen && platform.gameObject.transform.position.y < topOfScreen)
            return true;
        else {
            return false;
        }
    }
}
