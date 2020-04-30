using System.Collections;
using UnityEngine;

public class CoinMagnet : MonoBehaviour, IHaveSingleSound {

    [SerializeField]
    private MarketItem _marketItem = null;
    [SerializeField]
    private float _radius = 0f;
    [SerializeField]
    private float _duration = 0f;
    [SerializeField]
    private float _tempDuration = 0f;
    [SerializeField]
    private SphereCollider _collider = null;
    [SerializeField]
    private GameObject _coinMagnet = null;

    private bool isMagnetActive = false;

    private VFX _activeVFX;

    private void Start() {
        _marketItem.OnMarketItemUpdated += CalculateNewStats;
        CalculateNewStats();
        _tempDuration = _duration;
        _collider = GetComponentInChildren<SphereCollider>();
        _collider.radius = _radius;
        _coinMagnet.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "CoinMagnet") {
            PlayVFX();
            PlaySFX(SoundFXTypes.InGame_Collect_Slot_Powerup);
            other.gameObject.SetActive(false);

            if (isMagnetActive) {
                _tempDuration = _duration;
                return;
            }

            ActivateCoinMagnet();
            StartCoroutine(StopCoinMagnet());
        }
    }

    private void ActivateCoinMagnet() {
        isMagnetActive = true;
        _coinMagnet.SetActive(true);
    }

    private void PlayVFX() {
        _activeVFX = ObjectPooler.instance.SpawnFromPool(VFXTypes.VFXMagnet.ToString(), transform.position).GetComponent<VFX>();
        _activeVFX.SetTarget(this.transform);
        _activeVFX.Play();
    }

    private void StopVFX() {
        _activeVFX.Stop();
    }

    private void DeactivateCoinMagnet() {
        isMagnetActive = false;
        _coinMagnet.SetActive(false);
        _tempDuration = _duration;
    }

    private IEnumerator StopCoinMagnet() {
        while (true) {
            _tempDuration--;
            yield return new WaitForSeconds(1f);

            if (_tempDuration <= 0) {
                DeactivateCoinMagnet();
                StopVFX();
                break;
            }
        }
    }

    public void PlaySFX(SoundFXTypes sfxType) {
        ObjectPooler.instance.SpawnFromPool(sfxType.ToString(), transform.position);
    }

    private void CalculateNewStats() {
        _duration = _duration + _marketItem.GetCurrentLevel();
        _radius = _radius + (_marketItem.GetCurrentLevel() / 10);
    }

}
