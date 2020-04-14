using UnityEngine;
using UnityEngine.UI;

public class GameplayPanel : MonoBehaviour {

    [Header("Intializations")]
    [SerializeField]
    private Text _txtGold = null;
    [SerializeField]
    private Text _txtScore= null;

    private const string GOLD = "GOLD:\t";
    private const string SCORE = "SCORE:\t";

    private void Awake() {
        // TODO GET FROM DB.
        _txtGold.text = GOLD + 0;
        _txtScore.text = SCORE + 0;
    }

    private void Start() {
        Account.instance.OnPlayerStatsChanged += OnPlayerStatsChanged;
    }

    private void OnPlayerStatsChanged(PlayerStats playerStats) {
        _txtGold.text = GOLD + playerStats.GetGold().ToString();
        _txtScore.text = SCORE + playerStats.GetCurrentScore().ToString();
    }
}
