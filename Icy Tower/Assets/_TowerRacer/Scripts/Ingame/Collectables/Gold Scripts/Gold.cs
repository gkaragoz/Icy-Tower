using System;
using UnityEngine;

public class Gold : MonoBehaviour, IHaveSingleSound {

    private Transform _player = null;
    [SerializeField]
    private GameObject _yellowCoinPrefab; //Meshes
    [SerializeField]
    private GameObject _redCoinPrefab; //Meshes
    [SerializeField]
    private GameObject _greenCoinPrefab; //Meshes
    [SerializeField]
    private float _redCoinRates;
    [SerializeField]
    private float _greenCoinRates;

    private int _goldSelectorPossibilityRate = 1;

    [SerializeField]
    private int _coinAmount = 1;
    private float _flySpeedDivisor = 5f;
    private float _minFlySpeed = 0.4f;
    private bool _hasInteractedWithMagnet = false;
    private Vector3 _initialPosition = Vector3.zero;
    private Quaternion _initialQuaternion = Quaternion.identity;

    private void Awake() {
        _initialPosition = transform.localPosition;
        _initialQuaternion = transform.localRotation;
    }


    public void SetColorfulGoldValue(int value)
    {
        _goldSelectorPossibilityRate = value;
        _greenCoinRates = _redCoinRates = 5 * value;
    }

    private void FixedUpdate() {
        if (_hasInteractedWithMagnet) {
            FlyToPlayer();
        }
    }

    public void SetVisibility(bool isActive) {
        gameObject.SetActive(isActive);
        GetRandomCoin();
        if (isActive) {
            transform.localPosition = _initialPosition;
            transform.localRotation = _initialQuaternion;
            _hasInteractedWithMagnet = false;
        }
    }

    public void GetRandomCoin() {
        _redCoinRates += _greenCoinRates;

        float random = UnityEngine.Random.Range(0,100);
        if (random < _greenCoinRates) {
            _yellowCoinPrefab.SetActive(false);
            _redCoinPrefab.SetActive(false);
            _greenCoinPrefab.SetActive(true);
        }
        else if (random<_redCoinRates)
        {
            _redCoinPrefab.SetActive(true);
            _yellowCoinPrefab.SetActive(false);
            _greenCoinPrefab.SetActive(false);
        }
        else
        {
            _yellowCoinPrefab.SetActive(true);
            _redCoinPrefab.SetActive(false);
            _greenCoinPrefab.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "FullCollider") {
            PlayVFX();
            PlaySFX(SoundFXTypes.InGame_Collect_Gold);
            SetVisibility(false);

            if (MarketManager.instance.GetMarketItem(23).GetHasPermanentItemPurchased()) {
                Account.instance.AddVirtualCurrency(_coinAmount * 2, VirtualCurrency.Gold, false);
            } else {
                Account.instance.AddVirtualCurrency(_coinAmount, VirtualCurrency.Gold, false);
            }

            _hasInteractedWithMagnet = false;
        } else if (other.tag == "CoinMagnet") {
            _hasInteractedWithMagnet = true;
            _player = other.gameObject.transform;
        }
    }

    private void PlayVFX() {
        ObjectPooler.instance.SpawnFromPool(VFXTypes.VFXCollectGold.ToString(), transform.position);
    }

    private void FlyToPlayer() {
        transform.position = Vector3.MoveTowards(transform.position, _player.position, CalculateFlySpeed());
    }

    private float CalculateFlySpeed() {
        if (Vector3.Distance(transform.position, _player.position) / _flySpeedDivisor <= _minFlySpeed)
            return _minFlySpeed;
        return Vector3.Distance(transform.position, _player.position) / _flySpeedDivisor;
    }

    public void PlaySFX(SoundFXTypes sfxType) {
        ObjectPooler.instance.SpawnFromPool(sfxType.ToString(), transform.position);
    }
}
