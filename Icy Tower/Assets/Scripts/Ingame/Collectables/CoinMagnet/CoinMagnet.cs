using System.Collections;
using UnityEngine;

public class CoinMagnet : MonoBehaviour {

    [Utils.ReadOnly]
    [SerializeField]
    private CoinMagnetStats _coinMagnetStats = null;
    [Utils.ReadOnly]
    [SerializeField]
    private float _radius = 0f;
    [Utils.ReadOnly]
    [SerializeField]
    private float _duration = 0f;
    [SerializeField]
    private SphereCollider _collider = null;
    [SerializeField]
    private GameObject _coinMagnet = null;

    private VFX _activeVFX;

    private void Start() {
        _coinMagnetStats = GetComponent<CoinMagnetStats>();
        _collider = GetComponentInChildren<SphereCollider>();
        _radius = _coinMagnetStats.GetRadius();
        _collider.radius = _radius;
        _duration = _coinMagnetStats.GetDuration();
        _coinMagnet.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "CoinMagnet") {
            _duration = _coinMagnetStats.GetDuration();

            ActivateCoinMagnet();
            PlayVFX();

            StartCoroutine(StopCoinMagnet());
            other.gameObject.SetActive(false);
        }
    }

    private void ActivateCoinMagnet() {
        _coinMagnet.SetActive(true);
    }

    private void PlayVFX() {
        _activeVFX = Instantiate(VFXDatabase.instance.GetVFX(VFXTypes.Magnet), this.transform) as VFX;
        _activeVFX.transform.position = transform.position;
        _activeVFX.Play(true);
    }

    private void StopVFX() {
        _activeVFX.Stop();
    }

    private void DeactivateCoinMagnet() {
        _coinMagnet.SetActive(false);
    }

    private IEnumerator StopCoinMagnet() {
        while (true) {
            _duration--;
            yield return new WaitForSeconds(1f);

            if (_duration <= 0) {
                DeactivateCoinMagnet();
                StopVFX();
                break;
            }
        }
    }
}
