using UnityEngine;
using UnityEngine.UI;

public class WardrobePanelManagerUI : MonoBehaviour {

    [SerializeField]
    private WardrobeItemUI[] _btnBuys = null;

    [SerializeField]
    private Sprite _wardrobeSelected;
    [SerializeField]
    private Sprite _wardrobeNormal;
    [SerializeField]
    private Image[] _tabButtons;
    [SerializeField]
    private GameObject[] _wardrobeObjectPanel;
    [SerializeField]
    private Image[] _buttonInsideImage;

    private void Start() {
        Account.instance.OnPlayerStatsChanged += OnPlayerStatsChanged;

        Init();
    }

    private void OnPlayerStatsChanged(PlayerStats playerStats) {
        UpdateUI();
    }

    public void Init() {
        for (int ii = 0; ii < _btnBuys.Length; ii++) {
            _btnBuys[ii].Init();
        }

        UpdateUI();
    }

    public void UpdateUI() {
        for (int ii = 0; ii < _btnBuys.Length; ii++) {
            _btnBuys[ii].UpdateUI();
        }
    }

    public void ChooseTab(int index) {
        foreach (var item in _tabButtons) {
            item.sprite = _wardrobeNormal;
        }
        _tabButtons[index].sprite = _wardrobeSelected;

        foreach (var panel in _wardrobeObjectPanel) {
            panel.SetActive(false);

        }
        _wardrobeObjectPanel[index].SetActive(true);

        foreach (var buttonImage in _buttonInsideImage) {
            buttonImage.color = new Color32(126, 126, 126, 255);
        }

        _buttonInsideImage[index].color = new Color32(56, 107, 20, 255);
    }

}
