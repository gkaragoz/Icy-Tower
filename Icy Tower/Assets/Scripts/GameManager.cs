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
    // Start is called before the first frame update
    [SerializeField]
    private Transform _leftPlatformPivot;
    [SerializeField]
    private Transform _rightPlatformPivot;

    private float lastPlatformPositionY = 0;


    void Start()
    {
        ObjectPooler.instance.InitializePool("Platform");
    }

   

    public Transform LeftPlatformPivot { get { return _leftPlatformPivot; } }
    public Transform RightPlatformPivot { get { return _rightPlatformPivot; } }
}
