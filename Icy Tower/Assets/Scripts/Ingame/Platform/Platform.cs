using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlatformStats))]

public class Platform : MonoBehaviour, IPooledObject {

    [SerializeField]
    private GameObject[] _types = null;
    [SerializeField]
    private TextMeshProUGUI _floorCountText = null;

    public int Floor { get; set; }

    public TextMeshProUGUI FloorCountText {
        get { return _floorCountText; }
        set { _floorCountText = value; }
    }


    public void SetScale(Vector3 scale, int type) {
        _types[type].transform.localScale = scale;
    }

    public void SetPosition(Vector3 position, int type) {
        transform.position = position;
        _types[type].transform.localPosition = new Vector3(UnityEngine.Random.Range(_types[type].transform.localScale.z*2.2f,8- _types[type].transform.localScale.z * 2.6f),0,0); 
    }

    public void OnObjectReused() {
        gameObject.SetActive(true);
    }


    public void SetType(int platformTypeIndex) {
        if (platformTypeIndex >= _types.Length - 1)
            return;
        for (int i = 0; i < _types.Length; i++) {
            if (i == platformTypeIndex) {
                _types[i].SetActive(true);
            } else {
                _types[i].SetActive(false);
            }
        }
    }

    public void SetText() {
        if (Floor % 10 == 0) {
            _floorCountText.text = Floor.ToString();
            _floorCountText.gameObject.SetActive(true);
        } else {
            _floorCountText.gameObject.SetActive(false);
        }
    }
}
