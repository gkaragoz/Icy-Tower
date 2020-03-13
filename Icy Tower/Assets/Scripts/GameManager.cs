using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPooler))]
public class GameManager : MonoBehaviour
{

    #region Singleton

    public static GameManager instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion
    [SerializeField]
    private Transform _leftPlatformPivot = null;
    [SerializeField]
    private Transform _rightPlatformPivot =null;
    [SerializeField]
    private GameObject _pauseMenuCanvas= null;

    void Start()
    {
        ObjectPooler.instance.InitializePool("Platform");
        Time.timeScale = 1f;
        
    }

    public void PauseGame() {
        Time.timeScale = 0f;
        _pauseMenuCanvas.SetActive(true);
    }

    public void ContinueGame() {
        Time.timeScale = 1f;
        _pauseMenuCanvas.SetActive(false);
    }
   

    public Transform LeftPlatformPivot { get { return _leftPlatformPivot; } }
    public Transform RightPlatformPivot { get { return _rightPlatformPivot; } }
}
