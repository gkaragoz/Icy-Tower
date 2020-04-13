﻿using UnityEngine;

public class Sound : MonoBehaviour, IPooledObject {

    [SerializeField]
    private bool _isLoopable = false;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private AudioSource _sfx = null;
    [SerializeField]
    [Utils.ReadOnly]
    private float _duration = 0f;

    private void Awake() {
        _sfx = GetComponent<AudioSource>();
        _duration = _sfx.clip.length;
    }

    public void Play() {
        this.gameObject.SetActive(true);
        this._sfx.Play();
    }


    public void Stop() {
        this.gameObject.SetActive(false);
        this._sfx.Stop();
    }

    public void OnObjectReused() {
        if (_isLoopable)
            return;

        this.Play();
        Invoke("Stop", _duration);
    }

}