using UnityEngine;
using UnityEngine.UI;

public class GameplayPanel : MonoBehaviour {

    [Header("Intializations")]
    [SerializeField]
    private Text _txtGold = null;

    private const string GOLD = "GOLD:\t";

    private void Awake() {
        // TODO GET FROM DB.
        _txtGold.text = GOLD + 0;
    }

    private void Start() {
        GameManager.instance.OnPlayerStatsChanged += OnPlayerStatsChanged;
    }

    private void OnPlayerStatsChanged(PlayerStats playerStats) {
        _txtGold.text = GOLD + playerStats.GetGold().ToString();
    }
}
