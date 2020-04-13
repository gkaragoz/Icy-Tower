using System;
using UnityEngine;
[RequireComponent(typeof(PlatformStats))]

public class Platform : MonoBehaviour, IPooledObject {

    [SerializeField]
    private GameObject[] _types = null;

    public int Floor { get; set; }


    public void SetScale(Vector3 scale) {
        transform.localScale = scale;
    }

    public void SetPosition(Vector3 position) {

        transform.position = position;
    }

    public void OnObjectReused() {
        gameObject.SetActive(true);
    }

    public void SetType(int platformTypeIndex) {
        if (platformTypeIndex >= _types.Length - 1)
            return;
        for (int i = 0; i <_types.Length; i++) {
            if (i == platformTypeIndex) {
                _types[i].SetActive(true);
            } else {
                _types[i].SetActive(false);
            }
        }
    }
}
