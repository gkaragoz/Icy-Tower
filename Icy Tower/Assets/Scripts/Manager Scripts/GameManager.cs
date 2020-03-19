using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private Transform _leftMapSpawnTransform = null;
    [SerializeField]
    private Transform _rightMapSpawnTransform = null;



    public Transform LeftMapSpawnTransform { get { return _leftMapSpawnTransform; } }
    public Transform RightMapSpawnTransform { get { return _rightMapSpawnTransform; } }

    private void Start() {
        ObjectPooler.instance.InitializePool("Platform");
    }
}
