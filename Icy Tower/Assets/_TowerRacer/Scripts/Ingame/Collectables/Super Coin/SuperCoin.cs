using UnityEngine;

public class SuperCoin : MonoBehaviour, IPooledObject, IHaveSingleSound {

    [SerializeField]
    private MarketItem _marketItem= null;
    private int _currencyAmount = 25;

    private void Start() {
        _marketItem.OnMarketItemUpdated += CalculateNewStats;
        CalculateNewStats();
    }

    private void PlayVFX() {
        ObjectPooler.instance.SpawnFromPool(VFXTypes.VFXCollectGold.ToString(), transform.position);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "FullCollider") {
            PlayVFX();
            PlaySFX(SoundFXTypes.InGame_Collect_SuperGold);

            Account.instance.AddVirtualCurrency(_currencyAmount, VirtualCurrency.Gold, false);

            gameObject.SetActive(false);
        }
    }

    public void OnObjectReused() {
        gameObject.SetActive(true);
    }

    public void PlaySFX(SoundFXTypes sfxType) {
        ObjectPooler.instance.SpawnFromPool(sfxType.ToString(), transform.position);
    }

    private void CalculateNewStats() {
        _currencyAmount = _currencyAmount * _marketItem.GetCurrentLevel();
    }
}
