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

    void Start()
    {
        ObjectPooler.instance.InitializePool("Platform");
        
    }
   

    public Transform LeftPlatformPivot { get { return _leftPlatformPivot; } }
    public Transform RightPlatformPivot { get { return _rightPlatformPivot; } }
}
