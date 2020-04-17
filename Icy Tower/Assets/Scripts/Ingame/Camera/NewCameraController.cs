using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewCameraController : MonoBehaviour {

    [SerializeField]
    private CameraState[] _cameraStates = null;
    [SerializeField]
    private Transform _target = null;
    [SerializeField]
    private Transform _followers = null;
    [SerializeField]
    private float _deadZoneOffset = 0f;
    [SerializeField]
    private float _followersOffset = 11f;

    [SerializeField]
    private float _jumpOffset = 0f;

    public float speed = 2.0f;

    private bool _canMove = false;
    private bool _flyingUP = false;
    private bool _hasReachedStartFloor = false;
    public bool startMatch = false;
    public bool iCanRotate = false;
    private Coroutine mycor;

    private bool _isLeanTweenPlaying = false;

    private void Start() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        GameManager.instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1) {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _followers = GameObject.FindGameObjectWithTag("Collector").transform;
    }

    private void OnGameStateChanged(GameState previousState, GameState newState) {
        Debug.Log("Previous State: " + previousState);
        Debug.Log("New State: " + newState);

        LeanTween.cancelAll();

        foreach (CameraState cameraState in _cameraStates) {
            if (cameraState.state == GetCameraState(previousState, newState)) {
                Debug.Log("OYNAAAAAA " + cameraState.name);
                cameraState.Run(this.gameObject);
            }
        }

        if (newState == GameState.Gameplay) {
            startMatch = true;
        }
    }

    public void StartFollow() {
        _followers.transform.LeanMoveY(transform.position.y - _followersOffset, 2f).setOnComplete(()=> { 
            _followers.transform.SetParent(transform) ;
        });
    }

    private void Update() {
        if (_target == null) {
            return;
        }
        
        if (_followers == null) {
            return;
        }

        if (_isLeanTweenPlaying) {
            return;
        }

        if (GameManager.instance.GetGameState() == GameState.Gameplay) {
            if (HasReachedStartFloor()) {
                if (!_hasReachedStartFloor) {
                    StartFollow();
                    _hasReachedStartFloor = true;
                }

                //Check if i died
                if (_target.position.y < transform.position.y - _deadZoneOffset) {
                    transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(60, 0, 0), speed * Time.deltaTime);
                    _canMove = false;
                    startMatch = false;
                    GameManager.instance.HasPlayerDied = true;

                    if(Account.instance.GetKey() > 0) {
                        if(Input.GetKey(KeyCode.Space)) {
                            Revive();
                        }
                        return;
                    }
                    LevelManager.instance.OnClick_RestartGame();
                    return;
                }
            }

            if (startMatch) {
                Switcher();
            }

            if (_canMove) {
                NormalMovement();
            }

            if (_flyingUP) {
                FlyingUp();
            }
        }
    }

    private void Revive() {
        GameManager.instance.HasPlayerDied = false;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        _target.position = new Vector3(0,(Account.instance.GetCurrentScore() * 4) + PlatformManager.instance.InitialSpawnPosition ,_target.position.z);
        transform.position = new Vector3(0,_target.position.y, -12);
        _flyingUP = true;
        startMatch = true;
        _canMove = true;
        Account.instance.AddKey(-1);
        
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
            _flyingUP = true;
            _canMove = false;
        }

        if (_target.position.y < transform.position.y && HasReachedStartFloor()) {
            _flyingUP = false;
            _canMove = true;
        }
    }

    private bool HasReachedStartFloor() {
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

    private CameraStateEnums GetCameraState(GameState previousState, GameState targetState) {
        if (previousState == GameState.MainMenu && targetState == GameState.GameplayCountdown)
            return CameraStateEnums.MainMenu_to_Gameplay;
        if (previousState == GameState.Gameplay && targetState == GameState.MainMenu)
            return CameraStateEnums.Gameplay_to_MainMenu;
        if (previousState == GameState.MainMenu && targetState == GameState.Wardrobe)
            return CameraStateEnums.MainMenu_to_Wardrobe;
        if (previousState == GameState.Wardrobe && targetState == GameState.MainMenu)
            return CameraStateEnums.Wardrobe_to_MainMenu;
        if (previousState == GameState.Loading && targetState == GameState.MainMenu)
            return CameraStateEnums.MainMenu_to_MainMenu;

        return CameraStateEnums.None;
    }

}
