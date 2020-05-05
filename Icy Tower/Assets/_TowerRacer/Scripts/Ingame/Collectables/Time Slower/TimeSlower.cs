using System.Collections;
using UnityEngine;

public class TimeSlower : MonoBehaviour, IHaveSingleSound {

    [Header("DEBUG")]
    [SerializeField]
    private MarketItem _marketItem= null;
    [SerializeField]
    private float _duration = 0f;
    private float _tempDuration = 0f;


    private void Start() 
    {
        _marketItem = MarketManager.instance.GetMarketItem(_marketItem.GetId());

        _marketItem.OnMarketItemUpdated += CalculateNewStats;
        CalculateNewStats();
        _tempDuration = _duration;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "TimeSlower") {
            SlowTime();
            PlaySFX(SoundFXTypes.InGame_Collect_Slot_Powerup);
            other.gameObject.SetActive(false);
        }
    }

    private void SlowTime() {
        //TODO : divide cameraSpeed by _slowAmount;
    }

    private void SpeedUpTime() {
        //TODO : multiplie cameraSpeed by _slowAmount;
        _tempDuration = _duration;
    }


    private IEnumerator StopSlowingTime() {
        while (true) {
            _tempDuration--;
            yield return new WaitForSeconds(1f);

            if (_tempDuration <= 0) {
                SpeedUpTime();
                break;
            }
        }
    }

    public void PlaySFX(SoundFXTypes sfxType) {
        ObjectPooler.instance.SpawnFromPool(sfxType.ToString(), transform.position);
    }

    private void CalculateNewStats() {
        _duration =3+ _marketItem.GetCurrentLevel();
        _tempDuration = _duration;
    }
}
