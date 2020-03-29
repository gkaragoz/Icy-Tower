using System;
using UnityEngine;

public class Gold : MonoBehaviour {

    private Renderer _goldRenderer = null;
    private int _coinScore = 0;

    private string _goldType = "";

    public enum GoldTypes {
        YellowGold,
        GreenGold,
        BlueGold,
        RedGold
    }

    public void CreateGold() {
        _goldRenderer = GetComponent<Renderer>();
        SetGoldType();
        SetGoldValues();
    }

    private void SetGoldType() {
        int enumLenght = Enum.GetNames(typeof(GoldTypes)).Length;
        int _randomGoldType = UnityEngine.Random.Range(0, enumLenght);
        _goldType = Enum.GetName(typeof(GoldTypes), _randomGoldType);
    }

    private void SetGoldValues() {
        switch (_goldType) {
            case "YellowGold":
                _coinScore = 1;
                _goldRenderer.material.SetColor("_BaseColor", Color.yellow);
                break;
            case "GreenGold":
                _coinScore = 2;
                _goldRenderer.material.SetColor("_BaseColor", Color.green);
                break;
            case "BlueGold":
                _coinScore = 3;
                _goldRenderer.material.SetColor("_BaseColor", Color.blue);
                break;
            case "RedGold":
                _coinScore = 5;
                _goldRenderer.material.SetColor("_BaseColor", Color.red);
                break;
            default:
                Debug.LogError("Gold type did not exist");
                break;
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (other.tag == "FullCollider") {
            Destroy(gameObject);
            Debug.Log(_goldType + " " + _coinScore);
            GameManager.instance.AddGoldToPlayer(_coinScore);
        }
    }

    //TODO : Play Gold pickup Sound
}
