using System;
using UnityEngine;

public class Gold : MonoBehaviour {

    private Renderer _goldRenderer = null;
    private Transform _player = null;

    private float _flySpeed = 0.2f;
    private int _coinScore = 0;
    private string _goldType = "";
    private bool _hasInteractedWithMagnet = false;

    public void CreateGold() {
        _goldRenderer = GetComponent<Renderer>();
        SetGoldType();
        SetGoldValues();
    }

    private void FixedUpdate() {
        if (_hasInteractedWithMagnet) {
            FlyToPlayer();
        }
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
            gameObject.SetActive(false);
            GameManager.instance.AddGoldToPlayer(_coinScore);
            _hasInteractedWithMagnet = false;

        } else if (other.tag == "CoinMagnet") {
            _hasInteractedWithMagnet = true;
            _player = other.gameObject.transform;
        }
    }

    private void FlyToPlayer() {
        transform.position = Vector3.MoveTowards(transform.position, _player.position, _flySpeed);
    }

    //TODO : Play Gold pickup Sound
}
