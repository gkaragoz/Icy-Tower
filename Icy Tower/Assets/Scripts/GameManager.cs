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
    void Start()
    {
        ObjectPooler.instance.InitializePool("Platform");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
