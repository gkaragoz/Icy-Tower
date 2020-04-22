using UnityEngine;

public class VirtualCurrencyBar : MonoBehaviour {

    [SerializeField]
    private PlayerStats _playerStats = null;

    [SerializeField]
    private ShadowTextUpdater[] _txtGolds = null;
    [SerializeField]
    private ShadowTextUpdater[] _txtGems = null;
    [SerializeField]
    private ShadowTextUpdater[] _txtKeys = null;

    private void Start() {
        MarketManager.instance.OnBuyItem += UpdateUI;
        UpdateUI();
    }

    private void UpdateUI() {
        for (int ii = 0; ii < _txtGolds.Length; ii++) {
            _txtGolds[ii].SetText(_playerStats.GetGold().ToString());
            _txtGems[ii].SetText(_playerStats.GetGem().ToString());
            _txtKeys[ii].SetText(_playerStats.GetKey().ToString());
        }
    }

}
