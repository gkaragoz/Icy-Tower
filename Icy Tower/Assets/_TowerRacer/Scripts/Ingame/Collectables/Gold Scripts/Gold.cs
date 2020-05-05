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
        _greenCoinRates += _redCoinRates;
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

    public void GetRandomCoin()
    {
        float random = UnityEngine.Random.Range(0,100);
        if (random<_redCoinRates)
        {
            _redCoinPrefab.SetActive(true);
            _yellowCoinPrefab.SetActive(false);
            _greenCoinPrefab.SetActive(false);
            _coinAmount = 2;//+ Market Level gelecek
        }
        else if (random<_greenCoinRates)
        {
            _yellowCoinPrefab.SetActive(false);
            _redCoinPrefab.SetActive(false);
            _greenCoinPrefab.SetActive(true);
            _coinAmount = 3;//+ Market Level gelecek;

        }
        else
        {
            _yellowCoinPrefab.SetActive(true);
            _redCoinPrefab.SetActive(false);
            _greenCoinPrefab.SetActive(false);
            _coinAmount = 1;

        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "FullCollider") {
            PlayVFX();
            PlaySFX(SoundFXTypes.InGame_Collect_Gold);
            SetVisibility(false);

            Account.instance.AddVirtualCurrency(_coinAmount, VirtualCurrency.Gold, false);
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
