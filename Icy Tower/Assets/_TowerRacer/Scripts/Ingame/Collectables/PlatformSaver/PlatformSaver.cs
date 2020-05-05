using UnityEngine;

public class PlatformSaver : MonoBehaviour, IHaveSingleSound {

    [SerializeField]
    private MarketItem _marketItem= null;
    [SerializeField]
    private int _platformCountToMaximize = 0;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MaximizePlatformScale();
        }
    }

    private void Start() {
        _marketItem.OnMarketItemUpdated += CalculateNewStats;
        CalculateNewStats();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "PlatformSaver") {
            MaximizePlatformScale();
            PlaySFX(SoundFXTypes.InGame_PowerUp_BlockSaver);
            other.gameObject.SetActive(false);
        }
    }

    private void MaximizePlatformScale() {
        for (int i = 0; i < _platformCountToMaximize; i++) {
            Platform platformToMaximize = PlatformManager.instance.GetSpawnedPlatformAtFloor(Account.instance.GetCurrentScore() + i);
            int currentFloor = (int)(Account.instance.GetCurrentScore()/100);
            GameObject childPlatform = platformToMaximize.Types[currentFloor];
            if (childPlatform.activeSelf)
            {

            childPlatform.transform.localPosition = new Vector3(3.7f,0,0);
           // childPlatform.transform.localScale = new Vector3(childPlatform.transform.localScale.x, childPlatform.transform.localScale.y, 10f);
            childPlatform.transform.localScale = new Vector3(1, 1, 1.7f);
            childPlatform.GetComponentInParent<MovingPlatform>().StopMovement();
            Debug.Log("platformGirdim");
            }
            // childPlatform.transform.localPosition = Vector3.zero;
        }
    }


    public void PlaySFX(SoundFXTypes sfxType) {
        ObjectPooler.instance.SpawnFromPool(sfxType.ToString(), transform.position);
    }

    private void CalculateNewStats() {
        _platformCountToMaximize += _marketItem.GetCurrentLevel();
    }
}
