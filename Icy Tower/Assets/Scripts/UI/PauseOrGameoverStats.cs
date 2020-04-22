using TMPro;
using UnityEngine;

public class PauseOrGameoverStats : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI _txtScore = null;
    [SerializeField]
    private TextMeshProUGUI _txtCombo = null;
    [SerializeField]
    private TextMeshProUGUI _txtGold = null;

    public void SetText(string score, string combo, string gold) {
        _txtScore.text = score;
        _txtCombo.text = combo;
        _txtGold.text = gold;
    }

}
