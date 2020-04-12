using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

    [SerializeField]
    private AudioClip[] _clips = null;

    private AudioSource _sfx;

    private void Awake() {
        _sfx = GetComponent<AudioSource>();
    }

    private void Start() {
        int dice = Random.Range(0, 2);
        _sfx.clip = _clips[dice];
        _sfx.Play();
    }

}
