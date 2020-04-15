﻿using UnityEngine;

public class Rocket : MonoBehaviour{

    [SerializeField]
    private GameObject _player = null;
    [SerializeField]
    private GameObject _rocket= null;
    [SerializeField]
    private GameObject _playerGFX = null;

    [SerializeField]
    private int _floor = 200;

    private void OnEnable() {
        Debug.Log("rockeettt");
        _playerGFX.SetActive(false);
        _rocket.SetActive(true);
        LeanTween.moveY(_player, (_floor * 4) + 44, 20).setOnComplete(() => {
            _playerGFX.SetActive(true);
            _rocket.SetActive(false);
            gameObject.SetActive(false);
        });
    }

}
