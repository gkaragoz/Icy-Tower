using System.Collections;
using System.Linq;
using UnityEngine;

public class NewCameraController : MonoBehaviour {

    [System.Serializable]
    public class CameraStates {
        public GameState state;
        public Vector3 position;
        public Vector3 rotation;
        public float positionTime;
        public float rotationTime;
        public LeanTweenType easeType;
    }

    [SerializeField]
    private Transform _target = null;
    [SerializeField]
    private Transform _followers = null;
    [SerializeField]
    private float _deadZoneOffset = 0f;
    [SerializeField]
    private float _followersOffset = 11f;

    [SerializeField]
    private CameraStates[] _cameraStates = null;

    [SerializeField]
    private float _jumpOffset = 0f;

    public float speed = 2.0f;

    bool canMove = false;
    bool flyingUP = false;
    public bool startMatch = false;
    public bool iCanRotate = false;
    private Coroutine mycor;

    private bool _isLeanTweenPlaying = false;

    private void Start() {
        LevelManager.instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void MoveTo(GameState state) {
        CameraStates currentState = _cameraStates.Where(a => a.state == state).FirstOrDefault();

        LeanTween.move(this.gameObject, currentState.position, currentState.positionTime).setEase(currentState.easeType);
        LeanTween.rotate(this.gameObject, currentState.rotation, currentState.rotationTime).setEase(currentState.easeType);
    }

    private void OnGameStateChanged(GameState state) {
        switch (state) {
            case GameState.MainMenu:
                Debug.Log("MainMenu");
                MoveTo(state);
                break;
            case GameState.GamePaused:
                break;
            case GameState.NewGame:
                Debug.Log("NewGame");
                MoveTo(state);
                break;
            case GameState.RestartGame:
                break;
            case GameState.GameplayCountdown:
                break;
            case GameState.Gameplay:
                Debug.Log("Gameplay");
                startMatch = true;
                MoveTo(state);
                break;
            case GameState.GameOver:
                startMatch = false;
                break;
            default:
                break;
        }
    }

    private void Update() {
        if (_isLeanTweenPlaying) {
            return;
        }

        _followers.transform.position = new Vector3(_followers.transform.position.x, transform.position.y - _followersOffset, _followers.transform.position.z);

        //Check if i died
        if (_target.position.y < transform.position.y - _deadZoneOffset) {
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(60, 0, 0), speed * Time.deltaTime);
            return;
        }

        if (startMatch) {

            Switcher();
        }

        if (canMove) {
            NormalMovement();
        }

        if (flyingUP) {
            FlyingUp();
        }
    }

    void NormalMovement() {
        transform.Translate(0, speed * Time.deltaTime, 0);

    }

    void FlyingUp() {
        float interpolation = speed * Time.deltaTime;
        Vector3 position = this.transform.position;
        position.y = Mathf.Lerp(this.transform.position.y, _target.transform.position.y, interpolation * 2);
        this.transform.position = position;

    }

    void Switcher() {

        if (_target.position.y > transform.position.y + _jumpOffset) {
            flyingUP = true;
            canMove = false;
        }

        if (_target.position.y < transform.position.y && IsReachStartFloor()) {
            flyingUP = false;
            canMove = true;
        }
    }

    private bool IsReachStartFloor() {
        if (_target.position.y > PlatformManager.instance.GetSpawnedPlatformPositionAtFloor(20).y)
            return true;
        else
            return false;
    }

    public void WallWalk(int value) {
        if (mycor != null) {
            iCanRotate = false;
            StopCoroutine(mycor);

        }
        iCanRotate = true;
        mycor = StartCoroutine(GetPump(value));
    }

    IEnumerator GetPump(int side) {

        while (iCanRotate) {

            float interpolation = speed * Time.deltaTime;
            Vector3 rotation = this.transform.eulerAngles;
            rotation.y = 10 * side;

            if (side == 0) {
                rotation.x = 0;
                rotation.y = 0;
                rotation.z = 0;
            } else {
                rotation.x = -20;
            }


            this.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation), interpolation * 2);

            yield return null;
        }
    }

}


//T=x-2 R=y10 x-25