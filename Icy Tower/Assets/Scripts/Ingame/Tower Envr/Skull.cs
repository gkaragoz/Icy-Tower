using System;
using UnityEngine;

public class Skull : MonoBehaviour, IPooledObject {
    public void OnObjectReused() {
        gameObject.SetActive(true);
    }

}
