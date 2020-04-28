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
    public GameObject[] Types {
        get { return _types; }
    }
    public TextMeshProUGUI FloorCountText {
        get { return _floorCountText; }
        set { _floorCountText = value; }
    }

    
    public void SetScale(Vector3 scale, int type) {
        Vector3 tempVector = new Vector3( scale.x,scale.y,scale.z-(type*0.058f*2f));// Zorluk seviyesi kararına göre düzenlecek

        _types[type].transform.localScale = tempVector;
    }

    public void SetPosition(Vector3 position, int type) {
        Vector2 movePoints = new Vector2(_types[type].transform.localScale.z * 2.2f, 8 - _types[type].transform.localScale.z * 2.6f);
        transform.position = position;
        _types[type].transform.localPosition = new Vector3(UnityEngine.Random.Range(movePoints.x, movePoints.y), 0, 0);

        int randomRate1 = UnityEngine.Random.Range(1, 100);
        int randomRate = (type * 10) + ((type - 1) * 4);

        if (randomRate1 < randomRate) {
            _types[type].transform.GetComponent<MovingPlatform>().StartMovement(movePoints);// Gereken atama başlangıçta yapılabilir
        } else {
            _types[type].transform.GetComponent<MovingPlatform>().StopMovement();// Gereken atama başlangıçta yapılabilir (Test için buradalar düzenlenecekler)
        }
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
